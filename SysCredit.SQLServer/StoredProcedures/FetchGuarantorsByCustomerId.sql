CREATE PROCEDURE [dbo].[FetchGuarantorsByCustomerId]
	@CustomerID BIGINT
AS
BEGIN
  SELECT 
    [CT].[Identification],
    [CT].[Name],
    [CT].[LastName],
    [CT].[Address],
    [CT].[Neighborhood],
    [CT].[BussinessType], 
    [CT].[BussinessAddress],
    [CT].[Phone], 
    [RT].[Name]
  FROM .CustomerGuarantor AS CG
    INNER JOIN Customer AS CT
      ON CG.CustomerId = CT.CustomerId
    INNER JOIN Guarantor AS GT
      ON GT.GuarantorId = CG.GuarantorId
    INNER JOIN Relationship AS RT
      ON RT.RelationshipId = GT.RelationshipId
  WHERE ( CG.IsDelete = 0) AND CG.CustomerId = @CustomerID
END
