CREATE TYPE [dbo].[ReferenceType]
AS TABLE
(
    [Identification] NVARCHAR(16)  NULL, 
    [Name]           NVARCHAR(64)  NOT NULL, 
    [LastName]       NVARCHAR(64)  NOT NULL, 
    [Gender]         BIT           NOT NULL, 
    [Phone]          NVARCHAR(16)  NOT NULL, 
    [Email]          NVARCHAR(64)  NULL, 
    [Address]        NVARCHAR(256) NULL
);
GO
