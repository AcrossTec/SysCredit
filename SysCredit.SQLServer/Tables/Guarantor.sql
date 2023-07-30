CREATE TABLE [dbo].[Guarantor]
(
    [GuarantorId]      BIGINT        NOT NULL PRIMARY KEY IDENTITY, 
    [Identification]   NVARCHAR(16)  NOT NULL, 
    [Name]             NVARCHAR(64)  NOT NULL, 
    [LastName]         NVARCHAR(64)  NOT NULL, 
    [Gender]           BIT           NOT NULL, 
    [Email]            NVARCHAR(64)  NULL, 
    [Address]          NVARCHAR(256) NOT NULL, 
    [Neighborhood]     NVARCHAR(32)  NOT NULL, 
    [BussinessType]    NVARCHAR(32)  NOT NULL, 
    [BussinessAddress] NVARCHAR(256) NOT NULL, 
    [Phone]            NVARCHAR(16)  NOT NULL, 
    [RelationshipId]   BIGINT        NOT NULL, 
    [CreatedDate]      DATETIME2     NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate]     DATETIME2     NULL, 
    [DeletedDate]      DATETIME2     NULL, 
    [IsEdit]           BIT           NOT NULL DEFAULT 0, 
    [IsDelete]         BIT           NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Guarantor_RelationshipId_Relationship_RelationshipId] FOREIGN KEY ([RelationshipId]) REFERENCES [Relationship]([RelationshipId]), 
    CONSTRAINT [AK_Guarantor_Identification] UNIQUE ([Identification]), 
    CONSTRAINT [AK_Guarantor_Email]          UNIQUE ([Email]), 
    CONSTRAINT [AK_Guarantor_Phone]          UNIQUE ([Phone])
)
GO

CREATE INDEX [IX_Guarantor_Search_Fields] ON [dbo].[Guarantor] ([Identification], [Name], [LastName], [Phone])
GO
