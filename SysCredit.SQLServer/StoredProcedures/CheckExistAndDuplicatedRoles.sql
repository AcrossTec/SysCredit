CREATE PROCEDURE [dbo].[CheckExistAndDuplicatedRoles]
    @RoleNames [dbo].[AssignRequestType] READONLY
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @RoleCount INT;
    
    -- Contar la cantidad de nombres de roles únicos
    SELECT @RoleCount = COUNT(DISTINCT RoleName)
    FROM @RoleNames;
    
    IF @RoleCount = (SELECT COUNT(*) FROM [dbo].[Role] WHERE [Name] IN (SELECT RoleName FROM @RoleNames))
    BEGIN
        -- Todos los nombres de roles existen y no están duplicados
        SELECT 1 AS Result;
    END
    ELSE
    BEGIN
        -- No todos los nombres de roles existen o están duplicados
        SELECT 0 AS Result;
    END
END
