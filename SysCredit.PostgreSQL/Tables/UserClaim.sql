-- NUMBER: 1.13

CREATE TABLE IF NOT EXISTS "public"."UserClaim"
(
    "UserClaimId"  BIGSERIAL   PRIMARY KEY,
    "UserId"       BIGINT      NOT NULL,
    "ClaimType"    VARCHAR(30) NOT NULL,
    "ClaimValue"   VARCHAR(30) NOT NULL,
    "CreatedDate"  TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate" TIMESTAMP   NULL,
    "DeletedDate"  TIMESTAMP   NULL,
    "IsEdit"       BOOLEAN     NOT NULL DEFAULT FALSE,
    "IsDelete"     BOOLEAN     NOT NULL DEFAULT FALSE,
    CONSTRAINT "FK_UserClaim_User_UserId" FOREIGN KEY ("UserId") REFERENCES "public"."User"("UserId")
);