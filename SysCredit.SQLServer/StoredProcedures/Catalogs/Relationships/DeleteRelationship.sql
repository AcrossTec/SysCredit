CREATE PROCEDURE [dbo].[DeleteRelationship]
    @RelationshipId BIGINT
AS
BEGIN
    UPDATE [dbo].[Relationship]
    SET
        [IsEdit]		= 1,
        [IsDelete]		= 1,
        [ModifiedDate]	= CURRENT_TIMESTAMP,
        [DeletedDate]	= CURRENT_TIMESTAMP
    WHERE
        [RelationshipId] = @RelationshipId;
END
GO



