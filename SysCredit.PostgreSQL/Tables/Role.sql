CREATE TABLE public."Role"
(
    "RoleId"       BIGSERIAL   PRIMARY KEY,
    "Name"         VARCHAR(30) NOT NULL,
    "CreatedDate"  TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate" TIMESTAMP   NULL,
    "DeletedDate"  TIMESTAMP   NULL,
    "IsEdit"       BOOLEAN     NOT NULL DEFAULT FALSE,
    "IsDelete"     BOOLEAN     NOT NULL DEFAULT FALSE
);