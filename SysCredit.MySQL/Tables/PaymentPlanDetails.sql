-- Number: 12
CREATE TABLE IF NOT EXISTS `PaymentPlanDetails` (
    `PaymentPlanDetailId` BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `PaymentPlanId`       BIGINT         NOT NULL,
    `PaymentDate`         DATETIME       NOT NULL,
    `PaymentValue`        DECIMAL(22, 4) NOT NULL,
    `Balance`             DECIMAL(22, 4) NOT NULL,
    `CreatedDate`         DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate`        DATETIME,
    `DeletedDate`         DATETIME,
    `IsEdit`              BIT            NOT NULL DEFAULT 0,
    `IsDelete`            BIT            NOT NULL DEFAULT 0,
    CONSTRAINT `FK_PaymentPlanDetails_PaymentPlanId_PaymentPlan_PaymentPlanId` FOREIGN KEY (`PaymentPlanId`) REFERENCES `PaymentPlan`(`PaymentPlanId`)
);