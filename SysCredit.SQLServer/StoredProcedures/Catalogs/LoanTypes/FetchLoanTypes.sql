CREATE PROCEDURE [dbo].[FetchLoanTypes]
AS
BEGIN
	SELECT * FROM [dbo].[LoanType]
	WHERE [IsDelete] = 0
END
GO
