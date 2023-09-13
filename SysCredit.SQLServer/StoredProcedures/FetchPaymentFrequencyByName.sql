CREATE PROCEDURE [dbo].[FetchPaymentFrequencyByName]
	@Name NVARCHAR(32)
AS
BEGIN
	SELECT PF.*
	FROM [dbo].[PaymentFrequency] AS PF
	WHERE PF.[IsDelete] = 0 AND PF.[Name] = @Name
END
GO
