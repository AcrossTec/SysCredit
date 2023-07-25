CREATE PROCEDURE [dbo].[FetchGuarantorById]
	@GuarantorId BIGINT
AS
  SELECT 
  	[GT].[Name],
  	[GT].[LastName],
  	[GT].[Address],
  	[GT].[Neighborhood],
  	[GT].[BussinessType],
  	[GT].[BussinessAddress],
  	[GT].[Phone],
  	[RT].[Name]
  FROM Guarantor AS GT
  	INNER JOIN Relationship AS RT
  		ON GT.RelationshipId = RT.RelationshipId
  	WHERE GT.IsDelete = 0 AND GT.GuarantorId = @GuarantorId
RETURN 0
