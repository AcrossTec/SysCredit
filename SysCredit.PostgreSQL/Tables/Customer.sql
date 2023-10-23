CREATE TABLE public."Customer"
(
    "CustomerId"        BIGSERIAL    NOT NULL PRIMARY KEY,
    "Identification"    VARCHAR(16)  NOT NULL,
    "Name"              VARCHAR(64)  NOT NULL,
    "LastName"          VARCHAR(64)  NOT NULL,
    "Gender"            BIT          NOT NULL,
    "Email"             VARCHAR(64)  NULL,
    "Address"           VARCHAR(256) NOT NULL,
    "Neighborhood"      VARCHAR(32)  NOT NULL,
    "BussinessType"     VARCHAR(32)  NOT NULL,
    "BussinessAddress"  VARCHAR(256) NOT NULL,
    "Phone"             VARCHAR(16)  NOT NULL,
    "CreatedDate"       TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ModifiedDate"      TIMESTAMP    NULL,
    "DeleteDate"        TIMESTAMP    NULL,
    "IsEdit"            BOOLEAN      NOT NULL DEFAULT FALSE,
    "IsDelete"          BOOLEAN      NOT NULL DEFAULT FALSE,
    CONSTRAINT "AK_Customer_Identification" UNIQUE ("Identification"),
    CONSTRAINT "AK_Customer_Email"          UNIQUE ("Email"),
    CONSTRAINT "AK_Customer_Phone"          UNIQUE ("Phone")
);

CREATE INDEX "IX_Customer_Search_Fields"
ON public."Customer" ("Identification", "Name", "LastName", "Phone");