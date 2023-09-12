CREATE PROCEDURE [dbo].[FetchPaymentFrequency]
AS
BEGIN
	SELECT * FROM [dbo].[PaymentFrequency]
	WHERE [IsDelete] = 0
END
GO