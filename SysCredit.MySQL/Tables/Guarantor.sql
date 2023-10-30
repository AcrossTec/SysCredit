-- Number: 2
CREATE TABLE IF NOT EXISTS `Guarantor` (
    `GuarantorId`       BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Identification`    VARCHAR(16)    NOT NULL,
    `Name`              VARCHAR(64)    NOT NULL,
    `LastName`          VARCHAR(64)    NOT NULL,
    `Gender`            BIT            NOT NULL,
    `Email`             VARCHAR(64),
    `Address`           VARCHAR(256)   NOT NULL,
    `Neighborhood`      VARCHAR(32)    NOT NULL,
    `BussinessType`     VARCHAR(32)    NOT NULL,
    `BussinessAddress`  VARCHAR(256)   NOT NULL,
    `Phone`             VARCHAR(16)    NOT NULL,
    `CreatedDate`       DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate`      DATETIME,
    `DeletedDate`       DATETIME,
    `IsEdit`            BIT            NOT NULL DEFAULT 0,
    `IsDelete`          BIT            NOT NULL DEFAULT 0,
    CONSTRAINT `AK_Guarantor_Identification` UNIQUE (`Identification`),
    CONSTRAINT `AK_Guarantor_Email`          UNIQUE (`Email`),
    CONSTRAINT `AK_Guarantor_Phone`          UNIQUE (`Phone`)
);