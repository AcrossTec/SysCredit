CREATE TYPE "CustomerInfo" AS (
    "CustomerId"                BIGINT,
    "Identification"            VARCHAR(16),
    "Name"                      VARCHAR(64),
    "LastName"                  VARCHAR(64),
    "Gender"                    BIT,
    "Email"                     VARCHAR(64),
    "Address"                   VARCHAR(256),
    "Neighborhood"              VARCHAR(32),
    "BussinessType"             VARCHAR(32),
    "BussinessAddress"          VARCHAR(256),
    "Phone"                     VARCHAR(16),
    "CreatedDate"               TIMESTAMP,
    "ModifiedDate"              TIMESTAMP,
    "DeleteDate"                TIMESTAMP,
    "IsEdit"                    BOOLEAN,
    "IsDelete"                  BOOLEAN,
    "ReferenceId"               BIGINT,
    "ReferenceIdentification"   VARCHAR(16),
    "ReferenceName"             VARCHAR(64),
    "ReferenceLastName"         VARCHAR(64),
    "ReferenceGender"           BIT,
    "ReferencePhone"            VARCHAR(16),
    "ReferenceEmail"            VARCHAR(64),
    "ReferenceAddress"          VARCHAR(256),
    "GuarantorId"               BIGINT,
    "GuarantorIdentification"   VARCHAR(16),
    "GuarantorName"             VARCHAR(64),
    "GuarantorLastName"         VARCHAR(64),
    "GuarantorGender"           BIT,
    "GuarantorEmail"            VARCHAR(64),
    "GuarantorAddress"          VARCHAR(256),
    "GuarantorNeighborhood"     VARCHAR(32),
    "GuarantorBussinessType"    VARCHAR(32),
    "GuarantorBussinessAddress" VARCHAR(256),
    "GuarantorPhone"            VARCHAR(16),
    "GuarantorRelationshipId"   BIGINT,
    "GuarantorRelationshipName" VARCHAR(32)
);