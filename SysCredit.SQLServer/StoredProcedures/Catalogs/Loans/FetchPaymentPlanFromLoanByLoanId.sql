CREATE PROCEDURE [dbo].[FetchPaymentPlanFromLoanByLoanId]
    @LoanId BIGINT
AS
BEGIN
    SELECT
        PP.*,
        PPD.[PaymentPlanDetailId] AS [PaymentPlanDetailId],
        PPD.[PaymentDate]         AS [PaymentDate],
        PPD.[PaymentValue]        AS [PaymentValue],
        PPD.[Balance]             AS [PaymentPlanDetailBalance]
    FROM
        [dbo].[PaymentPlan] AS PP
    INNER JOIN
        [dbo].[PaymentPlanDetails] AS PPD
        ON PP.[PaymentPlanId] = PPD.[PaymentPlanId]
    WHERE
        PP.[IsDelete] = 0
        AND PP.[LoanId] = @LoanId;
END
GO
