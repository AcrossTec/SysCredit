CREATE PROCEDURE [dbo].[FetchPaymentPlanByLoanId]
    @LoanId BIGINT
AS
BEGIN
    SELECT
        *
    FROM
        [dbo].[PaymentPlan]
    WHERE
        [LoanId] = @LoanId
        AND [IsDelete] = 0;
END
GO
