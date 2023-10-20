CREATE TABLE [dbo].[CustomerReference]
(
    [CustomerReferenceId] BIGINT    NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId]          BIGINT    NOT NULL, 
    [ReferenceId]         BIGINT    NOT NULL, 
    [LoanId]              BIGINT    NULL, 
    [CreatedDate]         DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate]        DATETIME2 NULL, 
    [DeletedDate]         DATETIME2 NULL, 
    [IsEdit]              BIT       NOT NULL DEFAULT 0, 
    [IsDelete]            BIT       NOT NULL DEFAULT 0, 
    CONSTRAINT [AK_CustomerReference_CustomerId_ReferenceId_LoanId]     UNIQUE ([CustomerId], [ReferenceId], [LoanId]), 
    CONSTRAINT [FK_CustomerReference_CustomerId_Customer_CustomerId]    FOREIGN KEY ([CustomerId])       REFERENCES [Customer]([CustomerId]), 
    CONSTRAINT [FK_CustomerReference_ReferenceId_Reference_ReferenceId] FOREIGN KEY ([ReferenceId])      REFERENCES [Reference]([ReferenceId]), 
    CONSTRAINT [FK_CustomerReference_LoanId_Loan_LoanId]                FOREIGN KEY ([LoanId])           REFERENCES [Loan]([LoanId])
)
GO
