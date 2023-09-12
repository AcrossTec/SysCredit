CREATE TABLE [dbo].[UserClaim]
(
	[UserClaimId]  BIGINT       NOT NULL PRIMARY KEY IDENTITY,
	[UserId]       BIGINT       NOT NULL,
    [ClaimType]    NVARCHAR(30) NOT NULL,
    [ClaimValue]   NVARCHAR(30) NOT NULL,
    [CreatedDate]  DATETIME2    NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate] DATETIME2    NULL, 
    [DeletedDate]  DATETIME2    NULL, 
    [IsEdit]       BIT          NOT NULL DEFAULT 0, 
    [IsDelete]     BIT          NOT NULL DEFAULT 0,
    CONSTRAINT [FK_UserClaim_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
)
