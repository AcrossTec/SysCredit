CREATE PROCEDURE [dbo].[FetchUserById] @UserId BIGINT
AS
BEGIN
    SELECT U.* FROM [dbo].[User] AS U
    WHERE U.[IsDelete] = 0 AND U.[UserId] = @UserId
END 
GO
