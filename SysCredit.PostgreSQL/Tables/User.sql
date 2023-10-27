CREATE TABLE public."User"
(
    "UserId"       BIGSERIAL   PRIMARY KEY,
    "UserName"     VARCHAR(64) NOT NULL,
    "Email"        VARCHAR(64) NOT NULL,
    "Password"     TEXT        NOT NULL,
    "Phone"        VARCHAR(16) NOT NULL,
    "CreatedDate"  TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate" TIMESTAMP   NULL,
    "DeletedDate"  TIMESTAMP   NULL,
    "IsEdit"       BOOLEAN     NOT NULL DEFAULT FALSE,
    "IsDelete"     BOOLEAN     NOT NULL DEFAULT FALSE,
    CONSTRAINT "AK_User_UserName" UNIQUE ("UserName"),
    CONSTRAINT "AK_User_Email"    UNIQUE ("Email"),
    CONSTRAINT "AK_User_Phone"    UNIQUE ("Phone")
);