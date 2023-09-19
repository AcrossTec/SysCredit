CREATE PROCEDURE [dbo].[FetchLoanTypeById] @LoanTypeId BIGINT
AS
BEGIN
	SELECT * FROM [LoanType] WHERE  [IsDelete] = 0 AND @LoanTypeId = [LoanTypeId];
END
GO