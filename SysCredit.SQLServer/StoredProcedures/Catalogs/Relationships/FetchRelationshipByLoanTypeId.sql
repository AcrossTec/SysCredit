CREATE PROCEDURE [dbo].[FetchRelationshipByLoanTypeId] 
@LoanTypeId BIGINT
AS BEGIN
        SELECT R.* 
		FROM [dbo].[Relationship] R
		INNER JOIN [dbo].[CustomerGuarantor] CG ON R.[RelationshipId] = CG.[RelationshipId]
		INNER JOIN [dbo].[Loan] L ON CG.[LoanId] = L.[LoanId]
		INNER JOIN [dbo].[LoanType] LT ON L.[LoanTypeId] = LT.[LoanTypeId]
		WHERE R.[IsDelete] = 0
		AND LT.[LoanTypeId] = @LoanTypeId
END
GO