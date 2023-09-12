CREATE TABLE [dbo].[RoleUser]
(
	[RoleUserId]   BIGINT     NOT NULL PRIMARY KEY IDENTITY,
    [RoleId]       BIGINT     NOT NULL,
    [UserId]       BIGINT     NOT NULL,
	[CreatedDate]  DATETIME2  NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate] DATETIME2  NULL, 
    [DeletedDate]  DATETIME2  NULL, 
    [IsEdit]       BIT        NOT NULL DEFAULT 0, 
    [IsDelete]     BIT        NOT NULL DEFAULT 0,
    CONSTRAINT [FK_RoleUser_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_RoleUser_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role]([RoleId]), 

)
