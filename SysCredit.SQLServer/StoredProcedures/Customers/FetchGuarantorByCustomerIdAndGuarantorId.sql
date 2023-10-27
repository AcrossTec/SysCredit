CREATE PROCEDURE [dbo].[FetchGuarantorByCustomerIdAndGuarantorId]
    @CustomerId BIGINT,
    @GuarantorId BIGINT
AS
BEGIN
    SELECT TOP 1 
        G.*,
        R.[RelationshipId] AS RelationshipId,
        R.[Name]		   AS RelationshipName
    FROM [dbo].[Guarantor] AS G
    INNER JOIN [dbo].[CustomerGuarantor] AS CG ON G.[GuarantorId] = CG.[GuarantorId]
    INNER JOIN [dbo].[Relationship] AS R ON CG.[RelationshipId] = R.[RelationshipId]
    WHERE  G.[IsDelete]    = 0 
	  AND CG.[CustomerId]  = @CustomerId
	  AND  G.[GuarantorId] = @GuarantorId
END
GO
