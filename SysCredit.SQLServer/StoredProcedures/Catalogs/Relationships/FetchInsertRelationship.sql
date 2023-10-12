CREATE PROCEDURE [dbo].[InsertRelationship] 
	@Name			VARCHAR(32),
	@RelationshipId BIGINT OUTPUT
AS
BEGIN
	INSERT INTO [dbo].[Relationship]
	(
		[Name]
	)
	VALUES
	(
		@Name
	)
	SET @RelationshipId = SCOPE_IDENTITY();
END