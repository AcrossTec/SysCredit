CREATE TABLE [dbo].[Loan]
(
    [LoanId]                BIGINT         NOT NULL PRIMARY KEY IDENTITY, 
    [LoanTypeId]            BIGINT         NOT NULL, 
    [PaymentFrequencyId]    BIGINT         NOT NULL, 
    [CustomerId]            BIGINT         NOT NULL, 
    [LoanAmount]            DECIMAL(22, 4) NOT NULL, 
    [QtyPayments]           INT            NOT NULL, 
    [PaymentValue]          DECIMAL(22, 4) NOT NULL, 
    [FirstPaymentValue]     DECIMAL(22, 4) NOT NULL, 
    [LoanRate]              DECIMAL(22, 4) NOT NULL, 
    [TotalLoanAmount]       DECIMAL(22, 4) NOT NULL, 
    [PaymentAmountPerDay]   DECIMAL(22, 4) NOT NULL, 
    [PaymentAmountPerMonth] DECIMAL(22, 4) NOT NULL, 
    [DateFirstVisit]        DATETIME2      NOT NULL, 
    [DateEndPayment]        DATETIME2      NOT NULL, 
    [Notes]                 NVARCHAR(512)  NOT NULL,
    [LoanDate]              DATETIME2      NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [CreatedDate]           DATETIME2      NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedDate]          DATETIME2      NULL, 
    [DeletedDate]           DATETIME2      NULL, 
    [IsEdit]                BIT            NOT NULL DEFAULT 0 , 
    [IsDelete]              BIT            NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Loan_CustomerId_Customer_CustomerId]                         FOREIGN KEY ([CustomerId])         REFERENCES [Customer]([CustomerId]), 
    CONSTRAINT [FK_Loan_LoanTypeId_LoanType_LoanTypeId]                         FOREIGN KEY ([LoanTypeId])         REFERENCES [LoanType]([LoanTypeId]), 
    CONSTRAINT [FK_Loan_PaymentFrequencyId_PaymentFrequency_PaymentFrequencyId] FOREIGN KEY ([PaymentFrequencyId]) REFERENCES [PaymentFrequency]([PaymentFrequencyId])
)
GO
