DELETE FROM [dbo].[SectorCodes]
GO
DBCC CHECKIDENT ('dbo.SectorCodes',RESEED, 1)
GO

INSERT INTO [dbo].[SectorCodes]
  ([Code]
  ,[IsDeleted]
  ,[Name]
  ,[ParentId])
SELECT 
  [K_SEK]
  ,0
  ,[N_SEK]
  ,NULL
FROM [statcom].[dbo].[SPRSEK]
GO