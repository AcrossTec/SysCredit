CREATE PROCEDURE [dbo].[DeletePaymentFrequency]
	@PaymentFrequencyId BIGINT
AS
BEGIN
	UPDATE [dbo].[PaymentFrequency]
	SET
		[IsDelete]	   = 1,
		[IsEdit]	   = 1,
		[ModifiedDate] = CURRENT_TIMESTAMP,
		[DeletedDate]  = CURRENT_TIMESTAMP
	WHERE
		[PaymentFrequencyId] = @PaymentFrequencyId;
END
GO
