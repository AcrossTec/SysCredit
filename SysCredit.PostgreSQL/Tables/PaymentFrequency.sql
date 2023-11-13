-- NUMBER: 1.5

CREATE TABLE IF NOT EXISTS public."PaymentFrequency"
(
    "PaymentFrequencyId" BIGSERIAL   PRIMARY KEY,
    "Name"               VARCHAR(32) NOT NULL,
    "CreatedDate"        TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate"       TIMESTAMP   NULL,
    "DeletedDate"        TIMESTAMP   NULL,
    "IsEdit"             BOOLEAN     NOT NULL DEFAULT FALSE,
    "IsDelete"           BOOLEAN     NOT NULL DEFAULT FALSE,
    CONSTRAINT "AK_PaymentFrequency_Name" UNIQUE ("Name")
);