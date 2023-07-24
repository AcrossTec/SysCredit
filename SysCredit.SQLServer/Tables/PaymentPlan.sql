CREATE TABLE [dbo].[PaymentPlan]
(
    [PaymentPlanId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] BIGINT NOT NULL, 
    [LoanId] BIGINT NOT NULL, 
    [InitialBalance] DECIMAL(22, 4) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate] DATETIME2 NULL, 
    [DeletedDate] DATETIME2 NULL, 
    [IsEdit] BIT NOT NULL DEFAULT 0 , 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [AK_PaymentPlan_CustomerId_LoanId] UNIQUE ([CustomerId], [LoanId]), 
    CONSTRAINT [FK_PaymentPlan_LoanId_Loan_LoanId] FOREIGN KEY ([LoanId]) REFERENCES [Loan]([LoanId]), 
    CONSTRAINT [FK_PaymentPlan_CustomerId_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([CustomerId]), 
    CONSTRAINT [CK_PaymentPlan_CustomerId] CHECK ([dbo].[IsValidPaymentPlanCustomerId]([LoanId], [CustomerId]) = 1)
)
GO
