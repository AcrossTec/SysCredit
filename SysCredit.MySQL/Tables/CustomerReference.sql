-- Number: 10
CREATE TABLE IF NOT EXISTS `CustomerReference` (
    `CustomerReferenceId` BIGINT      NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `CustomerId`          BIGINT      NOT NULL,
    `ReferenceId`         BIGINT      NOT NULL,
    `LoanId`              BIGINT      NULL,
    `CreatedDate`         DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate`        DATETIME,
    `DeletedDate`         DATETIME,
    `IsEdit`              BIT         NOT NULL DEFAULT 0,
    `IsDelete`            BIT         NOT NULL DEFAULT 0,
    CONSTRAINT `AK_CustomerReference_CustomerId_ReferenceId_LoanId`     UNIQUE (`CustomerId`, `ReferenceId`, `LoanId`),
    CONSTRAINT `FK_CustomerReference_CustomerId_Customer_CustomerId`    FOREIGN KEY (`CustomerId`)  REFERENCES `Customer`(`CustomerId`),
    CONSTRAINT `FK_CustomerReference_ReferenceId_Reference_ReferenceId` FOREIGN KEY (`ReferenceId`) REFERENCES `Reference`(`ReferenceId`),
    CONSTRAINT `FK_CustomerReference_LoanId_Loan_LoanId`                FOREIGN KEY (`LoanId`)      REFERENCES `Loan`(`LoanId`)
);