﻿CREATE TABLE [dbo].[Reference]
(
    [ReferenceId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Identification] NVARCHAR(16) NULL, 
    [Name] NVARCHAR(64) NOT NULL, 
    [LastName] NVARCHAR(64) NOT NULL, 
    [Phone] NVARCHAR(16) NOT NULL, 
    [Email] NVARCHAR(64) NULL, 
    [Address] NVARCHAR(256) NULL
)
GO
