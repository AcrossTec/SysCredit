CREATE TABLE [dbo].[Role]
(
    [RoleId]       BIGINT       NOT NULL PRIMARY KEY IDENTITY,
    [Name]         NVARCHAR(30) NOT NULL,
    [CreatedDate]  DATETIME2    NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate] DATETIME2    NULL, 
    [DeletedDate]  DATETIME2    NULL, 
    [IsEdit]       BIT          NOT NULL DEFAULT 0, 
    [IsDelete]     BIT          NOT NULL DEFAULT 0,
)
GO
