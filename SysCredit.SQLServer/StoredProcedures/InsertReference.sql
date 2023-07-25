CREATE PROCEDURE [dbo].[InsertReference]
    @Name NVARCHAR(32)
AS
BEGIN
  INSERT INTO Relationship(Name)
  VALUES (@Name)
  SELECT SCOPE_IDENTITY();
END
