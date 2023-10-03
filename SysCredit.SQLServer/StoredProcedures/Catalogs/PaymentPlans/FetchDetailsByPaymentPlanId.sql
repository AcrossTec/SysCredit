CREATE PROCEDURE [dbo].[FetchDetailsByPaymentPlanId]
    @PaymentPlanId BIGINT
AS
BEGIN
    SELECT
        *
    FROM
        [dbo].[PaymentPlanDetails]
    WHERE
        [PaymentPlanId] = @PaymentPlanId
        AND [IsDelete] = 0;
END
GO
