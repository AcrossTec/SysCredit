CREATE TABLE public."CustomerReference"
(
    "CustomerReferenceId" BIGSERIAL PRIMARY KEY,
    "CustomerId"          BIGINT    NOT NULL,
    "ReferenceId"         BIGINT    NOT NULL,
    "LoanId"              BIGINT    NULL,
    "CreatedDate"         TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate"        TIMESTAMP NULL,
    "DeletedDate"         TIMESTAMP NULL,
    "IsEdit"              BOOLEAN   NOT NULL DEFAULT FALSE,
    "IsDelete"            BOOLEAN   NOT NULL DEFAULT FALSE,
    CONSTRAINT "AK_CustomerReference_CustomerId_ReferenceId_LoanId" UNIQUE ("CustomerId", "ReferenceId", "LoanId"),
	CONSTRAINT "FK_CustomerReference_LoanId_Loan_LoanId" FOREIGN KEY ("LoanId") REFERENCES "Loan"("LoanId")
);