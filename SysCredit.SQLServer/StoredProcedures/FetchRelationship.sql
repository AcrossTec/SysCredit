CREATE PROCEDURE [dbo].[FetchRelationship] 
AS
BEGIN
    SELECT * FROM Relationship WHERE (DeletedDate IS NULL AND IsDelete = 0)
END
