CREATE PROCEDURE [dbo].[FetchPaymentFrequencyById]
    @PaymentFrequencyId BIGINT
AS
BEGIN
    SELECT * FROM [dbo].[PaymentFrequency]
    WHERE [IsDelete] = 0 AND [PaymentFrequencyId] = @PaymentFrequencyId
END
GO