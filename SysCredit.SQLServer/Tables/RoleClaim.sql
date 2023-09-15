CREATE TABLE [dbo].[RoleClaim]
(
    [RoleClaimId]  BIGINT       NOT NULL PRIMARY KEY IDENTITY,
    [RoleId]       BIGINT       NOT NULL,
    [ClaimType]    NVARCHAR(30) NOT NULL,
    [ClaimValue]   NVARCHAR(30) NOT NULL,
    [CreatedDate]  DATETIME2    NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate] DATETIME2    NULL, 
    [DeletedDate]  DATETIME2    NULL, 
    [IsEdit]       BIT          NOT NULL DEFAULT 0, 
    [IsDelete]     BIT          NOT NULL DEFAULT 0,
    CONSTRAINT [FK_RoleClaim_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role]([RoleId]), 
)
GO