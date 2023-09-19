CREATE PROCEDURE [dbo].[FetchLogin]
	@Email NVARCHAR(64),
	@Password NVARCHAR(1024)
AS BEGIN 
	SELECT U.UserId, U.UserName, U.Email, U.Phone FROM [dbo].[User] AS U 
	WHERE U.[Password] = @Password AND U.[Email] = @Email AND U.[IsDelete] = 0
END
GO