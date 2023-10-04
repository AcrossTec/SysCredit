CREATE PROCEDURE [dbo].[FetchLoanByLoanId]
	@LoanId BIGINT
AS
BEGIN
	SELECT * FROM [dbo].[Loan] AS L
	WHERE L.[LoanId] = @LoanId
	AND L.[IsDelete] = 0
END
GO
