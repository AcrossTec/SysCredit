CREATE PROCEDURE [dbo].[FetchUserByEmail]
	@Email NVARCHAR(64)
AS BEGIN
	SELECT U.* FROM [dbo].[User] AS U
	WHERE U.[IsDelete] = 0 AND U.[Email] = @Email
END
GO