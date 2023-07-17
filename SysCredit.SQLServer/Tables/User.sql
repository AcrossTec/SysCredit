﻿CREATE TABLE [dbo].[User]
(
    [UserId] BIGINT NOT NULL PRIMARY KEY, 
    [UserName] NVARCHAR(64) NOT NULL, 
    [Email] NVARCHAR(64) NOT NULL, 
    [Password] NVARCHAR(60) NOT NULL, 
    [Phone] NVARCHAR(16) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate] DATETIME2 NULL, 
    [DeletedDate] DATETIME2 NULL, 
    [IsEdit] BIT NOT NULL DEFAULT 0 , 
    [IsDelete] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [AK_User_UserName] UNIQUE ([UserName]), 
    CONSTRAINT [AK_User_Email] UNIQUE ([Email]), 
    CONSTRAINT [AK_User_Phone] UNIQUE ([Phone])
)
GO