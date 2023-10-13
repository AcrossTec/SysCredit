CREATE PROCEDURE [dbo].[FetchRelationshipById] @RelationshipId BIGINT
AS 
BEGIN
    SELECT * FROM [dbo].[Relationship]
    WHERE [IsDelete] = 0 AND [RelationshipId] = @RelationshipId
END
GO
