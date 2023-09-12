CREATE PROCEDURE [dbo].[FetchRoleByName]
	@RoleName NVARCHAR(64)
AS BEGIN
	SELECT U.* FROM [dbo].[Role] AS U
	WHERE U.[IsDelete] = 0 AND U.[Name] = @RoleName
END
GO