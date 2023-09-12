CREATE PROCEDURE [dbo].[FetchPaymenFrequencyByName]
	@Name NVARCHAR(32)
AS
BEGIN
	SELECT F.* FROM  [dbo].[PaymentFrequency] AS F
	WHERE F.[IsDelete] = 0 AND F.[Name] = @Name
END
GO