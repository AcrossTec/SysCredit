CREATE PROCEDURE [dbo].[InsertPaymentFrequency]
	@Name				NVARCHAR(32)
	@PaymentFrequencyId BIGINT 
AS
BEGIN
     INSERT INTO [dbo].[PaymentFrequency]
	 (
		[Name]
	 )
	 VALUES
	 (
		@Name
	 )
	 SET @PaymentFrequencyId = SCOPE_IDENTITY()
END