CREATE TABLE public."PaymentPlan"
(
    "PaymentPlanId"    BIGSERIAL      PRIMARY KEY,
    "CustomerId"       BIGINT         NOT NULL,
    "LoanId"           BIGINT         NOT NULL,
    "InitialBalance"   DECIMAL(22, 4) NOT NULL,
    "CreatedDate"      TIMESTAMP      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate"     TIMESTAMP      NULL,
    "DeletedDate"      TIMESTAMP      NULL,
    "IsEdit"           BOOLEAN        NOT NULL DEFAULT FALSE,
    "IsDelete"         BOOLEAN        NOT NULL DEFAULT FALSE,
    CONSTRAINT "AK_PaymentPlan_CustomerId_LoanId" UNIQUE ("CustomerId", "LoanId"),
    CONSTRAINT "FK_PaymentPlan_LoanId_Loan_LoanId" FOREIGN KEY ("LoanId") REFERENCES public."Loan"("LoanId"),
    CONSTRAINT "FK_PaymentPlan_CustomerId_Customer_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES public."Customer"("CustomerId")
);