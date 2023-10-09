CREATE PROCEDURE [dbo].[FetchPaymentPlanByIdAndLoanId]
	@PaymentPlanId BIGINT,
	@LoanId        BIGINT
AS
BEGIN
	SELECT 
		PP.*,
		PPD.*
	FROM 
		[dbo].[PaymentPlan] AS PP
	INNER JOIN
		[dbo].[PaymentPlanDetails] AS PPD
		ON PPD.[PaymentPlanId] = @PaymentPlanId
	WHERE 
		PP.[IsDelete] = 0
	AND 
		PP.[PaymentPlanId] = @PaymentPlanId
	AND 
		PP.[LoanId] = @LoanId
END
GO