CREATE FUNCTION [dbo].[GetJsonGuarantorsByCustomerId] (@CustomerId BIGINT) RETURNS NVARCHAR(MAX)
AS BEGIN
   RETURN
   (
     SELECT G.*, RS.[RelationshipId], RS.[Name] AS [RelationshipName]
     FROM [dbo].[Guarantor] AS G
     INNER JOIN [dbo].[CustomerGuarantor] AS CG ON  G.[GuarantorId]    = CG.[GuarantorId]
     INNER JOIN [dbo].[Relationship]      AS RS ON CG.[RelationshipId] = RS.[RelationshipId]
     WHERE CG.[CustomerId] = @CustomerId
     FOR JSON AUTO, INCLUDE_NULL_VALUES
   )
END
