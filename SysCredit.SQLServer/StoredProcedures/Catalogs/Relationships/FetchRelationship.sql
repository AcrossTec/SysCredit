CREATE PROCEDURE [dbo].[FetchRelationship] 
AS BEGIN
    SELECT * FROM [dbo].[Relationship]
    WHERE [IsDelete] = 0
END
GO
