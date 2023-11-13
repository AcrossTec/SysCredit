-- Number: 3
CREATE TABLE IF NOT EXISTS `LoanType` (
    `LoanTypeId`   BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Name`         VARCHAR(32)  NOT NULL,
    `CreatedDate`  DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate` DATETIME,
    `DeletedDate`  DATETIME,
    `IsEdit`       BIT          NOT NULL DEFAULT 0,
    `IsDelete`     BIT          NOT NULL DEFAULT 0,
    CONSTRAINT `AK_LoanType_Name` UNIQUE (`Name`)
);