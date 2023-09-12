CREATE PROCEDURE [dbo].[FetchUserByName]
	@Name NVARCHAR(64)
AS BEGIN
	SELECT U.* FROM [dbo].[User] AS U
	WHERE U.[IsDelete] = 0 AND U.[UserName] = @Name
END
GO