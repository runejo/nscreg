DELETE FROM [dbo].[SectorCodes]
GO
DBCC CHECKIDENT ('dbo.SectorCodes',RESEED, 1)
GO

-- Strict insert, please edit insert values

INSERT INTO [dbo].[SectorCodes]
  ([Code],[IsDeleted],[Name],[NameLanguage1],[NameLanguage2],[ParentId])
VALUES
  ('Code1', 0,'Name', 'NameLanguage1', 'NameLanguage2', NULL),
  ('Code2', 0,'Name', 'NameLanguage1', 'NameLanguage2', NULL)
GO

-- OR select from other database

-- INSERT INTO [dbo].[SectorCodes]
--   ([Code],[IsDeleted],[Name],[NameLanguage1],[NameLanguage2],[ParentId])
-- SELECT 
--   [K_SEK]
--   ,0
--   ,[N_SEK]
--   ,NULL
--   ,NULL
--   ,NULL
-- FROM [statcom].[dbo].[SPRSEK]
-- GO
