CREATE TABLE [dbo].[CustomerGuarantor]
(
    [CustomerGuarantorId] BIGINT    NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId]          BIGINT    NOT NULL, 
    [GuarantorId]         BIGINT    NOT NULL, 
    [LoanId]              BIGINT    NULL, 
    [LoanDate]            DATETIME2 NULL, 
    [CreatedDate]         DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate]        DATETIME2 NULL, 
    [DeletedDate]         DATETIME2 NULL, 
    [IsEdit]              BIT       NOT NULL DEFAULT 0, 
    [IsDelete]            BIT       NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_CustomerGuarantor_CustomerId_Customer_CustomerId]    FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([CustomerId]), 
    CONSTRAINT [FK_CustomerGuarantor_GuarantorId_Guarantor_GuarantorId] FOREIGN KEY ([GuarantorId]) REFERENCES [Guarantor]([GuarantorId]), 
    CONSTRAINT [AK_CustomerGuarantor_CustomerId_GuarantorId_LoanId]     UNIQUE ([CustomerId], [GuarantorId], [LoanId])
)
GO
