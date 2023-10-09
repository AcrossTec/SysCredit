CREATE PROCEDURE [dbo].[UpdateRelationship] @RelationshipId BIGINT, @Name NVARCHAR(32)
AS
BEGIN
    UPDATE [dbo].[Relationship]
    SET [Name]         = @Name,
        [IsEdit]       = 1,
        [ModifiedDate] = CURRENT_TIMESTAMP
    WHERE
        [RelationshipId] = @RelationshipId
END
GO
