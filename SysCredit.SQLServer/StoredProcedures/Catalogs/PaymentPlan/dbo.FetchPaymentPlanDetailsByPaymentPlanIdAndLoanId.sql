CREATE PROCEDURE [dbo].[FetchPaymentPlanDetailsByPaymentPlanIdAndLoanId]
	@PaymentPlanId BIGINT,
	@LoanId BIGINT
AS
BEGIN
	SELECT PP.* FROM [dbo].[PaymentPlanDetails] AS PP
	INNER JOIN [dbo].[PaymentPlan] AS PPD ON PPD.[LoanId] = @LoanId
	WHERE PP.[IsDelete] = 0
	AND   PP.[PaymentPlanId] = @PaymentPlanId
END
GO