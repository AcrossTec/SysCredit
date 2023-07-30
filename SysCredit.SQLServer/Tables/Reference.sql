CREATE TABLE [dbo].[Reference]
(
    [ReferenceId]    BIGINT        NOT NULL PRIMARY KEY IDENTITY, 
    [Identification] NVARCHAR(16)  NULL, 
    [Name]           NVARCHAR(64)  NOT NULL, 
    [LastName]       NVARCHAR(64)  NOT NULL, 
    [Gender]         BIT           NOT NULL, 
    [Phone]          NVARCHAR(16)  NOT NULL, 
    [Email]          NVARCHAR(64)  NULL, 
    [Address]        NVARCHAR(256) NULL, 
    [CreatedDate]    DATETIME2     NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate]   DATETIME2     NULL, 
    [DeletedDate]    DATETIME2     NULL, 
    [IsEdit]         BIT           NOT NULL DEFAULT 0, 
    [IsDelete]       BIT           NOT NULL DEFAULT 0, 
)
GO
