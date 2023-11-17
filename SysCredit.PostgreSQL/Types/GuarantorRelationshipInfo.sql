CREATE TYPE "GuarantorRelationshipInfo" AS 
(
    "GuarantorId"      BIGINT,
    "Identification"   VARCHAR(16),
    "Name"             VARCHAR(64),
    "LastName"         VARCHAR(64),
    "Gender"           BIT,
    "Email"            VARCHAR(64),
    "Address"          VARCHAR(256),
    "Neighborhood"     VARCHAR(32),
    "BussinessType"    VARCHAR(32),
    "BussinessAddress" VARCHAR(256),
    "Phone"            VARCHAR(16),
    "CreatedDate"      TIMESTAMP,
    "ModifiedDate"     TIMESTAMP,
    "DeletedDate"      TIMESTAMP,
    "IsEdit"           BOOLEAN,
    "IsDelete"         BOOLEAN,
	
	-- Relationship
    "RelationshipId"   BIGINT,
    "RelationshipName" VARCHAR(32)
);