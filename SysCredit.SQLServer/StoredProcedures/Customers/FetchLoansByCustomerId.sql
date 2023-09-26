CREATE PROCEDURE [dbo].[FetchLoansByCustomerId] @CustomerId BIGINT
AS BEGIN
    SELECT 
        L.*,
        LT.[Name] AS [LoanTypeName],
        PF.[Name] AS [PaymentFrequencyName]
    FROM [dbo].[Loan] AS L
    INNER JOIN [dbo].[PaymentFrequency] AS PF ON PF.[PaymentFrequencyId] = L.[PaymentFrequencyId]
    INNER JOIN [dbo].[LoanType]         AS LT ON LT.[LoanTypeId]         = L.[LoanTypeId]
    INNER JOIN [dbo].[Customer]         AS  C ON  L.[CustomerId]         = C.[CustomerId]
    WHERE C.[IsDelete] = 0 AND C.[CustomerId] = @CustomerId;
END
GO
