CREATE TABLE [dbo].[PaymentPlan]
(
    [PaymentPlanId] BIGINT NOT NULL PRIMARY KEY, 
    [CustomerId] BIGINT NOT NULL, 
    [LoanId] BIGINT NOT NULL, 
    [InitialBalance] DECIMAL(22, 4) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate] DATETIME2 NULL, 
    [DeletedDate] DATETIME2 NULL, 
    [IsEdit] BIT NOT NULL DEFAULT 0 , 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [AK_PaymentPlan_CustomerId_LoanId] UNIQUE ([CustomerId], [LoanId])
)
GO
