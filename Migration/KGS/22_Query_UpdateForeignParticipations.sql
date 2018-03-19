TRUNCATE TABLE [dbo].[ForeignParticipations]

INSERT INTO ForeignParticipations
  (Name, Code, IsDeleted)
VALUES
  ('Иностранное участие стран СНГ', '3', 0),
  ('Иностранное участие стран вне СНГ', '4', 0),
  ('Иностранное участие стран СНГ и стран вне СНГ', '5', 0),
  ('Иностранное физическое, юридическое лицо, организация стран СНГ', '6', 0),
  ('Иностранное физическое, юридическое лицо, организация стран вне СНГ', '7', 0),
  ('Иностранное физическое, юридическое лицо, организация стран СНГ и стран вне СНГ', '8', 0),
  ('Международная организация', '9', 0)