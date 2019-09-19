DELETE FROM [dbo].[DictionaryVersions]
GO
DBCC CHECKIDENT ('dbo.DictionaryVersions',RESEED, 1)
GO

INSERT INTO [dbo].[DictionaryVersions] ([VersionId],[VersionName])
VALUES (1,'Migration version')
GO
  
DELETE FROM [dbo].[ActivityCategories]
GO
DBCC CHECKIDENT ('dbo.ActivityCategories',RESEED, 1)
GO

ALTER TABLE [dbo].[ActivityCategories] ADD OldParentId NVARCHAR(20) NULL
GO

INSERT INTO [dbo].[ActivityCategories]
  ([Code]
  ,[IsDeleted]
  ,[Name]
  ,[ParentId]
  ,[Section]
  ,[OldParentId]
  ,[VersionId]
  ,[DicParentId]
  ,[ActivityCategoryLevel])
SELECT   
  [KOD]
  ,0
  ,[NAME]
  ,0
  ,[KODA]
  ,[PARENT_ID]
  ,1
  ,NULL
  ,NULL
FROM [statcom].[dbo].[OKED3]
GO

UPDATE aco
SET [DicParentId] = aco.Id
FROM [dbo].[ActivityCategories] aco
GO

UPDATE aco
SET [ParentId] = ac.Id
FROM [dbo].[ActivityCategories] ac
  INNER JOIN [dbo].[ActivityCategories] aco
    ON ac.Code = aco.OldParentId
GO

ALTER TABLE [dbo].[ActivityCategories] DROP COLUMN OldParentId
GO

-- Add a function that gets the level number, passing the ID
-- The activitycategoryLevel is used to create statistical reports
CREATE FUNCTION GetActivityCategoryLevel (@input_id INT)   
  RETURNS INT
AS BEGIN   
  DECLARE @in_id INT = @input_id;
  DECLARE @level INT = 1;

  WHILE @in_id > 0 
  BEGIN
    SELECT top 1 @in_id = ParentId FROM ActivityCategories WHERE Id = @in_id
    IF @in_id > 0 SET @level = @level + 1;
  END

  RETURN @level  
END  
GO

-- Update ActivityCategories with correct level number
UPDATE ActivityCategories
SET ActivityCategoryLevel = dbo.GetActivityCategoryLevel(Id)
GO

-- Remove unnecessary function
DROP FUNCTION [dbo].[GetActivityCategoryLevel]