-- NUMBER: 1.8

CREATE TABLE IF NOT EXISTS public."CustomerGuarantor"
(
    "CustomerGuarantorId" BIGSERIAL PRIMARY KEY,
    "CustomerId"          BIGINT    NOT NULL,
    "GuarantorId"         BIGINT    NOT NULL,
    "RelationshipId"      BIGINT    NOT NULL,
    "LoanId"              BIGINT    NULL,
    "LoanDate"            TIMESTAMP NULL,
    "CreatedDate"         TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate"        TIMESTAMP NULL,
    "DeletedDate"         TIMESTAMP NULL,
    "IsEdit"              BOOLEAN   NOT NULL DEFAULT FALSE,
    "IsDelete"            BOOLEAN   NOT NULL DEFAULT FALSE,
    CONSTRAINT "FK_CustomerGuarantor_CustomerId_Customer_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES public."Customer"("CustomerId"),
    CONSTRAINT "FK_CustomerGuarantor_GuarantorId_Guarantor_GuarantorId" FOREIGN KEY ("GuarantorId") REFERENCES public."Guarantor"("GuarantorId"),
    CONSTRAINT "FK_CustomerGuarantor_RelationshipId_Relationship_RelationshipId" FOREIGN KEY ("RelationshipId") REFERENCES public."Relationship"("RelationshipId"),
    CONSTRAINT "FK_CustomerGuarantor_LoanId_Loan_LoanId" FOREIGN KEY ("LoanId") REFERENCES public."Loan"("LoanId"),
    CONSTRAINT "AK_CustomerGuarantor_CustomerId_GuarantorId_LoanId" UNIQUE ("CustomerId", "GuarantorId", "LoanId")
);