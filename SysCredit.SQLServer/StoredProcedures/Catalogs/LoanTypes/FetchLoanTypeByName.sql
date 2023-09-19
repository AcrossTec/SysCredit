CREATE PROCEDURE [dbo].[FetchLoanTypeByName]
	@Name NVARCHAR(32)
AS
BEGIN
	SELECT T.*
	FROM [dbo].[LoanType] AS T
	WHERE T.[IsDelete] = 0 AND T.[Name] = @Name
END
GO
