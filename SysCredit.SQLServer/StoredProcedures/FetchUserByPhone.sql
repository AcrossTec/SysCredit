CREATE PROCEDURE [dbo].[FetchUserByPhone]
	@Phone NVARCHAR(64)
AS BEGIN
	SELECT U.* FROM [dbo].[User] AS U
	WHERE U.[IsDelete] = 0 AND U.[Phone] = @Phone
END
GO