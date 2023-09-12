CREATE PROCEDURE [dbo].[InsertRole]
	@RoleId BIGINT OUTPUT,
	@Name   NVARCHAR(30),
	@RoleClaims [dbo].[ClaimRequestType] READONLY
AS BEGIN
    SET NOCOUNT ON
    BEGIN TRANSACTION

    INSERT INTO [dbo].[Role] ([Name])
    VALUES (@Name)
    	
	SET @RoleId = SCOPE_IDENTITY();

    INSERT INTO [dbo].[RoleClaim]([RoleId], [ClaimType], [ClaimValue])
    SELECT @RoleId, [ClaimType], [ClaimValue] FROM @RoleClaims;

    COMMIT TRANSACTION;
END
GO