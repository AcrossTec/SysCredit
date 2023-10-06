CREATE PROCEDURE [dbo].[FetchRelationshipByLoanTypeId] 
@LoanTypeId BIGINT
AS BEGIN
        SELECT R.*
		FROM [dbo].[Relationship] AS R
		INNER JOIN [dbo].[CustomerGuarantor] AS CG ON R.[RelationshipId] = CG.[RelationshipId]
		INNER JOIN [dbo].[Loan]              AS L  ON CG.[LoanId] = L.[LoanId]
		INNER JOIN [dbo].[LoanType]          AS LT ON L.[LoanTypeId] = LT.[LoanTypeId]
		WHERE R.[IsDelete] = 0 AND LT.[LoanTypeId] = @LoanTypeId
END
GO
