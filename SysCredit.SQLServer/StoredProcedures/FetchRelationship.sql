CREATE PROCEDURE [dbo].[FetchRelationship] 
AS
BEGIN
    SELECT * FROM Relationship WHERE IsDelete = 0
END
