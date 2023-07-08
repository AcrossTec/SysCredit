CREATE TABLE [dbo].[PaymentPlanDetails]
(
    [PaymentPlanDetailId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [PaymentPlanId] BIGINT NOT NULL, 
    [PaymentDate] DATETIME2 NOT NULL, 
    [PaymentValue] DECIMAL(22, 4) NOT NULL, 
    [Balance] DECIMAL(22, 4) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate] DATETIME2 NULL, 
    [DeletedDate] DATETIME2 NULL, 
    [IsEdit] BIT NOT NULL DEFAULT 0 , 
    [IsDelete] BIT NOT NULL DEFAULT 0
)
GO
