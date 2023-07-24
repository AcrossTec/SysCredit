CREATE TABLE [dbo].[CustomerReference]
(
    [CustomerReferenceId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] BIGINT NOT NULL, 
    [ReferenceId] BIGINT NOT NULL, 
    [LoanId] BIGINT NULL, 
    CONSTRAINT [AK_CustomerReference_CustomerId_ReferenceId_LoanId] UNIQUE ([CustomerId], [ReferenceId], [LoanId])
)
GO
