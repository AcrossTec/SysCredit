-- NUMBER: 1.7

CREATE TABLE IF NOT EXISTS public."Loan"
(
    "LoanId"                BIGSERIAL      PRIMARY KEY,
    "LoanTypeId"            BIGINT         NOT NULL,
    "PaymentFrequencyId"    BIGINT         NOT NULL,
    "CustomerId"            BIGINT         NOT NULL,
    "LoanAmount"            DECIMAL(22, 4) NOT NULL,
    "QtyPayments"           INT            NOT NULL,
    "PaymentValue"          DECIMAL(22, 4) NOT NULL,
    "FirstPaymentValue"     DECIMAL(22, 4) NOT NULL,
    "LoanRate"              DECIMAL(22, 4) NOT NULL,
    "TotalLoanAmount"       DECIMAL(22, 4) NOT NULL,
    "PaymentAmountPerDay"   DECIMAL(22, 4) NOT NULL,
    "PaymentAmountPerMonth" DECIMAL(22, 4) NOT NULL,
    "DateFirstVisit"        TIMESTAMP      NOT NULL,
    "DateEndPayment"        TIMESTAMP      NOT NULL,
    "Notes"                 VARCHAR(512)   NOT NULL,
    "LoanDate"              TIMESTAMP      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "CreatedDate"           TIMESTAMP      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate"          TIMESTAMP      NULL,
    "DeletedDate"           TIMESTAMP      NULL,
    "IsEdit"                BOOLEAN        NOT NULL DEFAULT FALSE,
    "IsDelete"              BOOLEAN        NOT NULL DEFAULT FALSE,
    CONSTRAINT "FK_Loan_CustomerId_Customer_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES public."Customer"("CustomerId"),
    CONSTRAINT "FK_Loan_LoanTypeId_LoanType_LoanTypeId" FOREIGN KEY ("LoanTypeId") REFERENCES public."LoanType"("LoanTypeId"),
    CONSTRAINT "FK_Loan_PaymentFrequencyId_PaymentFrequency_PaymentFrequencyId" FOREIGN KEY ("PaymentFrequencyId") REFERENCES public."PaymentFrequency"("PaymentFrequencyId")
);