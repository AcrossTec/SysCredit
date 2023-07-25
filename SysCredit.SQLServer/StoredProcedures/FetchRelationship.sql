CREATE PROCEDURE [dbo].[FetchRelationship] 
AS
BEGIN
    SELECT [RelationshipId], [Name] FROM Relationship WHERE (IsDelete = 0)
END
