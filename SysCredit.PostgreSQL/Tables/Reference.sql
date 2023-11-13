-- NUMBER: 1.4

CREATE TABLE IF NOT EXISTS public."Reference"
(
    "ReferenceId"    BIGSERIAL    PRIMARY KEY,
    "Identification" VARCHAR(16)  NULL,
    "Name"           VARCHAR(64)  NOT NULL,
    "LastName"       VARCHAR(64)  NOT NULL,
    "Gender"         BIT          NOT NULL,
    "Phone"          VARCHAR(16)  NOT NULL,
    "Email"          VARCHAR(64)  NULL,
    "Address"        VARCHAR(256) NULL,
    "CreatedDate"    TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate"   TIMESTAMP    NULL,
    "DeletedDate"    TIMESTAMP    NULL,
    "IsEdit"         BOOLEAN      NOT NULL DEFAULT FALSE,
    "IsDelete"       BOOLEAN      NOT NULL DEFAULT FALSE
);