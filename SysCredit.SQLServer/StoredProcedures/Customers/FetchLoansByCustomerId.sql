CREATE PROCEDURE [dbo].[FetchLoansByCustomerId] @CustomerId BIGINT
AS
BEGIN
   SELECT LT.[Name] FROM [dbo].[Loan] AS L
   INNER JOIN [dbo].[LoanType] AS LT
   On L.[LoanTypeId] = LT.[LoanTypeId]
   INNER JOIN [dbo].[Customer] AS C
   ON L.[CustomerId] = C.[CustomerId]
    WHERE C.[IsDelete] = 0 AND
C.[CustomerId] = @CustomerId;
END
GO