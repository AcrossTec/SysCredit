CREATE PROCEDURE [dbo].[FetchLoansByCustomerId] @CustomerId BIGINT
AS
BEGIN
   Select LT.[Name] From [dbo].[Loan] AS L
   INNER JOIN [dbo].[LoanType] AS LT
   On L.[LoanTypeId] = LT.[LoanTypeId]
   INNER JOIN [dbo].[Customer] AS C
   ON L.[CustomerId] = C.[CustomerId]
    WHERE C.[CustomerId] = 1;
END
GO