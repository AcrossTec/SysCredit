-- NUMBER: 1.16

CREATE TABLE IF NOT EXISTS "public"."UserRole"
(
    "RoleUserId"    BIGSERIAL   PRIMARY KEY,
    "RoleId"        BIGINT      NOT NULL,
    "UserId"        BIGINT      NOT NULL,
    "CreatedDate"   TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate"  TIMESTAMP   NULL,
    "DeletedDate"   TIMESTAMP   NULL,
    "IsEdit"        BOOLEAN     NOT NULL DEFAULT FALSE,
    "IsDelete"      BOOLEAN     NOT NULL DEFAULT FALSE,
    CONSTRAINT "FK_RoleUser_User_UserId" FOREIGN KEY ("UserId") REFERENCES "public"."User"("UserId"),
    CONSTRAINT "FK_RoleUser_Role_RoleId" FOREIGN KEY ("RoleId") REFERENCES "public"."Role"("RoleId")
);