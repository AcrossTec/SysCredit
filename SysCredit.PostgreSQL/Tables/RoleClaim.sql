-- NUMBER: 1.6

CREATE TABLE IF NOT EXISTS "public"."RoleClaim"
(
    "RoleClaimId"  BIGSERIAL   PRIMARY KEY,
    "RoleId"       BIGINT      NOT NULL,
    "ClaimType"    VARCHAR(30) NOT NULL,
    "ClaimValue"   VARCHAR(30) NOT NULL,
    "CreatedDate"  TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate" TIMESTAMP   NULL,
    "DeletedDate"  TIMESTAMP   NULL,
    "IsEdit"       BOOLEAN     NOT NULL DEFAULT FALSE,
    "IsDelete"     BOOLEAN     NOT NULL DEFAULT FALSE,
    CONSTRAINT "FK_RoleClaim_Role_RoleId" FOREIGN KEY ("RoleId") REFERENCES "public"."Role"("RoleId")
);