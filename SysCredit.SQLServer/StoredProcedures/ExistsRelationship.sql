CREATE PROCEDURE [dbo].[ExistsRelationship] @RelationshipId BIGINT
AS BEGIN
    RETURN IIF(EXISTS(SELECT * FROM [dbo].[Relationship] WHERE [IsDelete] = 0 AND [RelationshipId] = @RelationshipId), 1, 0)
END
GO
