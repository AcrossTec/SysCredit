CREATE PROCEDURE [dbo].[FetchPaymentPlanById]
    @PaymentPlanId BIGINT
AS
BEGIN
    SELECT
        *
    FROM
        [dbo].[PaymentPlan]
    WHERE
        [IsDelete] = 0 AND
        [PaymentPlanId] = @PaymentPlanId;
END
GO
