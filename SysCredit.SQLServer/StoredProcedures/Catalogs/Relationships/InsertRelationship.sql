CREATE PROCEDURE [dbo].[InsertRelationship] 
    @Name           VARCHAR(32),
    @RelationShipId BIGINT OUT
AS
BEGIN
    INSERT INTO [dbo].[Relationship]([Name])
    VALUES(@Name)
    SET @RelationShipId = SCOPE_IDENTITY()
END
GO