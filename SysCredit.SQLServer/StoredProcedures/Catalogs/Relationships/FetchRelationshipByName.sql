CREATE PROCEDURE [dbo].[FetchRelationshipByName] @Name NVARCHAR(32)
AS
BEGIN
    SELECT R.*
    FROM [dbo].[Relationship] AS R
    WHERE R.[IsDelete] = 0 AND R.[Name] = @Name
END
GO