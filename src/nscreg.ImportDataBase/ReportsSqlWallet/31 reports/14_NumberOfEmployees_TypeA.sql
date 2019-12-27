/* Table A would get all top level regions
    Row headers - Number of employees
    Column headers - Regions
 */
/*
	RegionLevel for kyrgyz database:
		1 Level : Kyrgyz Republic - Country level
		2 Level : Area, Oblast, Region, Counties
		3 Level : Rayon
		4 Level : City / Village
    Note: if you haven't region level for country Region/Counties etc would be 1 Level
*/

/* Input parameters from report body - filters that have to be defined by the user */
BEGIN
	DECLARE @InStatusId NVARCHAR(MAX) = $StatusId,
			@InStatUnitType NVARCHAR(MAX) = $StatUnitType,
			@InCurrentYear NVARCHAR(MAX) = YEAR(GETDATE())
END

/* Declare variables */
BEGIN
DECLARE @cols AS NVARCHAR(MAX),
		@query AS NVARCHAR(MAX),
		@totalSumCols AS NVARCHAR(MAX),
		@activityCategoryLevel AS NVARCHAR(MAX),
		@regionLevel AS NVARCHAR(MAX),
		@selectCols AS NVARCHAR(MAX)


SET @selectCols = STUFF((SELECT distinct ','+QUOTENAME(r.Name)
      /* Set RegionLevel IN (1) if there no Country Level in the Regions database */
			FROM Regions r  WHERE RegionLevel IN (1,2)
			FOR XML PATH(''), TYPE
			).value('.', 'NVARCHAR(MAX)')
		,1,1,'')

/* Column - REGIONS, COUNTRY LEVEL */
SET @cols = STUFF((SELECT distinct ',ISNULL(' + QUOTENAME(r.Name)+',0) AS "' + r.Name + '"'
            /* RegionLevel IN (1) - if there no Country Level in the Regions database */
            FROM Regions r  WHERE RegionLevel IN (1,2)
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)')
        ,1,1,'')

/* Column - Total count of employees by whole country */
SET @totalSumCols = STUFF((SELECT distinct '+ISNULL(' + QUOTENAME(r.Name)+',0)'
            /* RegionLevel IN (1) - if there no Country Level in the Regions database */
            FROM Regions r  WHERE RegionLevel IN (1,2)
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)')
        ,1,1,'')

SET @activityCategoryLevel = 1

/* @regionLevel = 1 - if there no Country Level in the Regions database */
SET @regionLevel = 2
END

/* Declare and fill Activity Categories Tree*/
BEGIN

/* Delete a temporary table #tempActivityCategories if exists */
IF (OBJECT_ID('tempdb..#tempActivityCategories') IS NOT NULL)
BEGIN DROP TABLE #tempActivityCategories END

/* Create a temporary table #tempActivityCategories */
CREATE TABLE #tempActivityCategories
(
    ID INT,
    Level INT,
    ParentId INT,
    Name NVARCHAR(MAX)
);
/* Create an index "ix_tempActivityCategoriesIndex" - to make search faster - Activity Categories */
CREATE NONCLUSTERED INDEX ix_tempActivityCategoriesIndex ON #tempActivityCategories ([ID]);

/* using CTE (Common Table Expressions), recursively collect the Activity Categories tree */
;WITH ActivityCategoriesCTE AS (
	SELECT
		Id, 0 AS Level, CAST(Id AS VARCHAR(255)) AS ParentId
	FROM ActivityCategories

	UNION ALL

	SELECT
		i.Id, Level + 1, CAST(itms.ParentId AS VARCHAR(255))
	FROM ActivityCategories i
	INNER JOIN ActivityCategoriesCTE itms ON itms.Id = i.ParentId
),

/* Select all levels from Activity Categories and order them */
CTE_RN AS
(
    SELECT Id,Level,ParentId, ROW_NUMBER() OVER (PARTITION BY r.Id ORDER BY r.Level DESC) RN
    FROM ActivityCategoriesCTE r
)

/* Fill with data the temporary table #tempActivityCategories */
INSERT INTO #tempActivityCategories
SELECT r.Id, r.RN, r.ParentId, rp.Name AS ParentName
FROM CTE_RN r
INNER JOIN ActivityCategories rp ON rp.Id = r.ParentId
INNER JOIN ActivityCategories rc ON rc.Id = r.Id
WHERE r.RN = @activityCategoryLevel

END

/* Declare and fill Regions Tree*/
BEGIN

/* Delete a temporary #tempRegions table if exists */
IF (OBJECT_ID('tempdb..#tempRegions') IS NOT NULL)
	BEGIN DROP TABLE #tempRegions END

/* Create a new temporary table for Regions */
CREATE TABLE #tempRegions
(
    ID INT,
    Level INT,
    ParentId INT,
    Name NVARCHAR(MAX)
);

/* Create an index "ix_tempRegionsIndex" - to make search faster - Regions */
CREATE NONCLUSTERED INDEX ix_tempRegionsIndex ON #tempRegions ([ID]);

/* using CTE (Common Table Expressions), recursively collect the Regions tree */
;WITH RegionsCTE AS (
	SELECT Id, 0 AS Level, CAST(Id AS VARCHAR(255)) AS ParentId
	FROM Regions

	UNION ALL

	SELECT i.Id, Level + 1, CAST(itms.ParentId AS VARCHAR(255))
	FROM Regions i
	INNER JOIN RegionsCTE itms ON itms.Id = i.ParentId
	WHERE i.ParentId>0
),

/* Select all levels from Regions and order them */
CTE_RN2 AS
(
    SELECT Id,Level,ParentId, ROW_NUMBER() OVER (PARTITION BY r.Id ORDER BY r.Level DESC) RN
    FROM RegionsCTE r
)

/* Fill with data the temporary table #tempRegions */
INSERT INTO #tempRegions
SELECT r.Id, r.RN, r.ParentId, rp.Name AS ParentName
FROM CTE_RN2 r
	INNER JOIN Regions rp ON rp.Id = r.ParentId
	INNER JOIN Regions rc ON rc.Id = r.Id
WHERE r.RN = @regionLevel OR r.Id = 1
END


/*
The resulting query
At the first checking the history logs that have StartPeriod less than current year
and then
Activity Categories top-level select
and then
ResultTable - get the actual state of statistical units where RegistrationDate and StartPeriod less than current year
and then
Join Activity Categories to select statistical units
and then
Sum of Employees value and using the pivot - transform from regions column to regions row
*/
SET @query = '
;WITH StatisticalUnitHistoryCTE AS (
	SELECT
		RegId,
		ParentId,
		AddressId,
		UnitStatusId,
		Discriminator,
		Employees,
		ROW_NUMBER() over (partition by ParentId order by StartPeriod desc) AS RowNumber
	FROM StatisticalUnitHistory
	WHERE DATEPART(YEAR,StartPeriod)<'+@InCurrentYear+'
),
ActivityCategoriesForResultCTE AS
(
	SELECT Id,Name
	FROM dbo.ActivityCategories
	WHERE ActivityCategoryLevel = 1
),
ResultTableCTE AS
(
	SELECT
		IIF(DATEPART(YEAR, su.RegistrationDate)<'+@InCurrentYear+' AND DATEPART(YEAR,su.StartPeriod)<'+@InCurrentYear+',ac.Name,ach.Name) AS Name,
		IIF(DATEPART(YEAR, su.RegistrationDate)<'+@InCurrentYear+' AND DATEPART(YEAR,su.StartPeriod)<'+@InCurrentYear+',ac.ParentId,ach.ParentId) AS ActivityCategoryId,
		IIF(DATEPART(YEAR, su.RegistrationDate)<'+@InCurrentYear+' AND DATEPART(YEAR,su.StartPeriod)<'+@InCurrentYear+',su.RegId, asuhCTE.RegId) AS RegId,
		IIF(DATEPART(YEAR, su.RegistrationDate)<'+@InCurrentYear+' AND DATEPART(YEAR,su.StartPeriod)<'+@InCurrentYear+',su.AddressId,asuhCTE.AddressId) AS AddressId,
		IIF(DATEPART(YEAR, su.RegistrationDate)<'+@InCurrentYear+' AND DATEPART(YEAR,su.StartPeriod)<'+@InCurrentYear+',su.UnitStatusId,asuhCTE.UnitStatusId) AS UnitStatusId,
		IIF(DATEPART(YEAR, su.RegistrationDate)<'+@InCurrentYear+' AND DATEPART(YEAR,su.StartPeriod)<'+@InCurrentYear+',su.Discriminator,asuhCTE.Discriminator) AS Discriminator,
		IIF(DATEPART(YEAR, su.RegistrationDate)<'+@InCurrentYear+' AND DATEPART(YEAR,su.StartPeriod)<'+@InCurrentYear+',su.Employees,asuhCTE.Employees) AS EmployeeAmount,
		IIF(DATEPART(YEAR, su.RegistrationDate)<'+@InCurrentYear+' AND DATEPART(YEAR,su.StartPeriod)<'+@InCurrentYear+',0,1) AS isHistory
	FROM StatisticalUnits AS su
		LEFT JOIN ActivityStatisticalUnits asu ON asu.Unit_Id = su.RegId
		LEFT JOIN Activities a ON a.Id = asu.Activity_Id AND a.Activity_Type = 1
		LEFT JOIN #tempActivityCategories AS ac ON ac.Id = a.ActivityCategoryId
		
		LEFT JOIN StatisticalUnitHistoryCTE asuhCTE ON asuhCTE.ParentId = su.RegId and asuhCTE.RowNumber = 1
		LEFT JOIN ActivityStatisticalUnitHistory asuh ON asuh.Unit_Id = asuhCTE.RegId
		LEFT JOIN Activities ah ON ah.Id = asuh.Activity_Id AND ah.Activity_Type = 1
		LEFT JOIN #tempActivityCategories AS ach ON ach.Id = ah.ActivityCategoryId
),
ResultTableCTE2 AS
(
	SELECT
		RegId,
		tr.Name AS NameOblast,
		ActivityCategoryId,
		EmployeeAmount
	FROM ResultTableCTE AS rt
		LEFT JOIN dbo.Address AS addr ON addr.Address_id = rt.AddressId
		INNER JOIN #tempRegions AS tr ON tr.Id = addr.Region_id

	WHERE (''' + @InStatUnitType + ''' = ''All'' OR (rt.isHistory = 0 AND  rt.Discriminator = ''' + @InStatUnitType + ''') 
				OR (rt.isHistory = 1 AND rt.Discriminator = ''' + @InStatUnitType + 'History' + '''))
			AND ('+@InStatusId+' = 0 OR rt.UnitStatusId = '+@InStatusId+')
)

SELECT Name, ' + @totalSumCols + ' as Total, ' + @cols + ' from
            (
				SELECT
					acrc.Name,
					rt.NameOblast,
					rt.EmployeeAmount
				FROM ActivityCategoriesForResultCTE as acrc
				LEFT JOIN ResultTableCTE2 AS rt ON acrc.Id = rt.ActivityCategoryId
           ) SourceTable
            PIVOT
            (
                SUM(EmployeeAmount)
                FOR NameOblast IN (' + @selectCols + ')
            ) PivotTable order by Name'
/* execution of the query */
execute(@query)
