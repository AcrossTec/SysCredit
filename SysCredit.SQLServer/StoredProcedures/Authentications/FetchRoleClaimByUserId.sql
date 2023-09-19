CREATE PROCEDURE [dbo].[FetchRoleClaimByUserId] @UserId BIGINT
AS
BEGIN
    SELECT RC.ClaimType, RC.ClaimValue
    FROM [dbo].[RoleClaim] AS RC
    INNER JOIN [dbo].[Role] AS R ON RC.RoleId = R.RoleId
    INNER JOIN [dbo].[UserRole] AS RU ON R.RoleId = RU.RoleId
    WHERE RU.UserId = @UserId
        AND R.[IsDelete]  = 0
        AND RC.[IsDelete] = 0;
END
GO
