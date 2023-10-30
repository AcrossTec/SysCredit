-- Number: 12
CREATE TABLE IF NOT EXISTS `PaymentPlan` (
    `PaymentPlanId`  BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `CustomerId`     BIGINT         NOT NULL,
    `LoanId`         BIGINT         NOT NULL,
    `InitialBalance` DECIMAL(22, 4) NOT NULL,
    `CreatedDate`    DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate`   DATETIME,
    `DeletedDate`    DATETIME,
    `IsEdit`         BIT            NOT NULL DEFAULT 0,
    `IsDelete`       BIT     	    NOT NULL DEFAULT 0,
    CONSTRAINT `AK_PaymentPlan_CustomerId_LoanId`              UNIQUE (`CustomerId`, `LoanId`),
    CONSTRAINT `FK_PaymentPlan_LoanId_Loan_LoanId`             FOREIGN KEY (`LoanId`)     REFERENCES `Loan`(`LoanId`),
    CONSTRAINT `FK_PaymentPlan_CustomerId_Customer_CustomerId` FOREIGN KEY (`CustomerId`) REFERENCES `Customer`(`CustomerId`)
);