CREATE PROCEDURE [dbo].[FetchClaimByUserId]
	@UserId BIGINT
AS BEGIN
	SELECT UC.ClaimType, UC.ClaimValue FROM [dbo].[User] AS U
	INNER JOIN [dbo].[UserClaim] AS UC
	ON U.[UserId] = UC.[UserId]
	WHERE U.[IsDelete] = 0 AND UC.[IsDelete] = 0 AND U.[UserId] = @UserId
END 
GO