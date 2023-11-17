CREATE TYPE "public"."ReferenceType" AS
(
    "Identification" VARCHAR(16),
	"Name"           VARCHAR(64),
	"LastName"       VARCHAR(64),
	"Gender"         BIT,
	"Phone"          VARCHAR(16),
	"Email"          VARCHAR(64),
	"Address"        VARCHAR(254)
);