CREATE PROCEDURE [dbo].[FetchGuarantorsByCustomerId] @CustomerId BIGINT
AS BEGIN
	SELECT G.*
	FROM [dbo].[Guarantor] AS G
	INNER JOIN [dbo].[CustomerGuarantor] AS CG
	ON G.[GuarantorId] = CG.[GuarantorId]
	WHERE G.[IsDelete] = 0 
	AND CG.[CustomerId] = @CustomerId
END
GO
