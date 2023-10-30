-- Number: 10
CREATE TABLE IF NOT EXISTS `CustomerGuarantor` (
    `CustomerGuarantorId` BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `CustomerId`          BIGINT         NOT NULL,
    `GuarantorId`         BIGINT         NOT NULL,
    `RelationshipId`      BIGINT         NOT NULL,
    `LoanId`              BIGINT,
    `LoanDate`            DATETIME,
    `CreatedDate`         DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate`        DATETIME,
    `DeletedDate`         DATETIME,
    `IsEdit`              BIT            NOT NULL DEFAULT 0,
    `IsDelete`            BIT            NOT NULL DEFAULT 0,
    CONSTRAINT `FK_CustomerGuarantor_CustomerId_Customer_CustomerId`             FOREIGN KEY (`CustomerId`)     REFERENCES `Customer`(`CustomerId`),
    CONSTRAINT `FK_CustomerGuarantor_GuarantorId_Guarantor_GuarantorId`          FOREIGN KEY (`GuarantorId`)    REFERENCES `Guarantor`(`GuarantorId`),
    CONSTRAINT `FK_CustomerGuarantor_RelationshipId_Relationship_RelationshipId` FOREIGN KEY (`RelationshipId`) REFERENCES `Relationship`(`RelationshipId`),
    CONSTRAINT `FK_CustomerGuarantor_LoanId_Loan_LoanId`                         FOREIGN KEY (`LoanId`)         REFERENCES `Loan`(`LoanId`),
    UNIQUE `AK_CustomerGuarantor_CustomerId_GuarantorId_LoanId` (`CustomerId`, `GuarantorId`, `LoanId`)
);