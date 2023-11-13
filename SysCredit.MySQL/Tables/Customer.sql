-- Number: 1
CREATE TABLE IF NOT EXISTS `Customer` (
    `CustomerId`       BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Identification`   VARCHAR(16)    NOT NULL,
    `Name`             VARCHAR(64)    NOT NULL,
    `LastName`         VARCHAR(64)    NOT NULL,
    `Gender`           BIT            NOT NULL,
    `Email`            VARCHAR(64),
    `Address`          VARCHAR(256)   NOT NULL,
    `Neighborhood`     VARCHAR(32)    NOT NULL,
    `BussinessType`    VARCHAR(32)    NOT NULL,
    `BussinessAddress` VARCHAR(256)   NOT NULL,
    `Phone`            VARCHAR(16)    NOT NULL,
    `CreatedDate`      DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate`     DATETIME,
    `DeletedDate`      DATETIME,
    `IsEdit`           BIT            NOT NULL DEFAULT 0,
    `IsDelete`         BIT            NOT NULL DEFAULT 0,
    CONSTRAINT `AK_Customer_Identification` UNIQUE (`Identification`),
    CONSTRAINT `AK_Customer_Email`          UNIQUE (`Email`),
    CONSTRAINT `AK_Customer_Phone`          UNIQUE (`Phone`),
    INDEX `IX_Customer_Search_Fields` (`Identification`, `Name`, `LastName`, `Phone`)
);