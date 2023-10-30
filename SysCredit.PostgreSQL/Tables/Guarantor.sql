-- NUMBER: 1.2

CREATE TABLE IF NOT EXISTS "public"."Guarantor"
(
    "GuarantorId"      BIGSERIAL    PRIMARY KEY,
    "Identification"   VARCHAR(16)  NOT NULL,
    "Name"             VARCHAR(64)  NOT NULL,
    "LastName"         VARCHAR(64)  NOT NULL,
    "Gender"           BIT          NOT NULL,
    "Email"            VARCHAR(64)  NULL,
    "Address"          VARCHAR(256) NOT NULL,
    "Neighborhood"     VARCHAR(32)  NOT NULL,
    "BussinessType"    VARCHAR(32)  NOT NULL,
    "BussinessAddress" VARCHAR(256) NOT NULL,
    "Phone"            VARCHAR(16)  NOT NULL,
    "CreatedDate"      TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate"     TIMESTAMP    NULL,
    "DeletedDate"      TIMESTAMP    NULL,
    "IsEdit"           BOOLEAN      NOT NULL DEFAULT FALSE,
    "IsDelete"         BOOLEAN      NOT NULL DEFAULT FALSE,
    CONSTRAINT "AK_Guarantor_Identification" UNIQUE ("Identification"),
    CONSTRAINT "AK_Guarantor_Email"          UNIQUE ("Email"),
    CONSTRAINT "AK_Guarantor_Phone"          UNIQUE ("Phone")
);

CREATE INDEX "IX_Guarantor_Search_Fields" ON "public"."Guarantor" ("Identification", "Name", "LastName", "Phone");
