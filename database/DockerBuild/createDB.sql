USE Master;
GO
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MakersOfDenmark')
BEGIN
CREATE DATABASE MakersOfDenmark
END;
GO