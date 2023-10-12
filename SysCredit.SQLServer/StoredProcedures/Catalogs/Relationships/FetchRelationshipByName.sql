CREATE PROCEDURE [dbo].[FetchRelationshipByName] @Name NVARCHAR(32)
AS
BEGIN
    SELECT *
    FROM [dbo].[Relationship]
    WHERE [IsDelete] = 0 AND [Name] = @Name
END
GO