CREATE PROCEDURE [dbo].[FetchLoanByPaymentFrequencyId]
	@PaymentFrequencyId BIGINT
AS
BEGIN
	SELECT * FROM [dbo].[Loan]
	WHERE [IsDelete] = 0 AND [PaymentFrequencyId] = @PaymentFrequencyId
END
GO