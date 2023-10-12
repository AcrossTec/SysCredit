CREATE PROCEDURE [dbo].[DeleteRelationship]
	@RelationshipId BIGINT
AS
BEGIN
	UPDATE [dbo].[Relationship]
	SET
		[IsEdit]		= 1,
		[IsDelete]		= 1,
		[ModifiedDate]	= CURRENT_TIMESTAMP,
		[ModifiedDate]	= CURRENT_TIMESTAMP
	WHERE
		[RelationshipId] = @RelationshipId AND [IsDelete] = 0;
END
GO



