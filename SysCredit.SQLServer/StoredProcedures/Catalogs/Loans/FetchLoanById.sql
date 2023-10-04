CREATE PROCEDURE [dbo].[FetchLoanByLoanId]
    @LoanId BIGINT
AS
BEGIN
    SELECT 
        L.*,
        LT.*,
        PF.*,
        C.*
    FROM 
        [dbo].[Loan] AS L
    INNER JOIN 
        [dbo].[LoanType] AS LT ON L.[LoanTypeId] = LT.[LoanTypeId]
    INNER JOIN 
        [dbo].[PaymentFrequency] AS PF ON L.[PaymentFrequencyId] = PF.[PaymentFrequencyId]
    INNER JOIN 
        [dbo].[Customer] AS C ON L.[CustomerId] = C.[CustomerId]
    WHERE 
        L.[LoanId] = @LoanId
        AND L.[IsDelete] = 0;
END
GO
