CREATE PROCEDURE [dbo].[FetchLoanTypeById]
	@LoanTypeId BIGINT
AS
BEGIN
	SELECT T.* 
	FROM [dbo].[LoanType] AS T
	WHERE T.[IsDelete] = 0 
	AND T.[LoanTypeId] = @LoanTypeId
END
GO
