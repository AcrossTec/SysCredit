-- Number: 14
CREATE TABLE IF NOT EXISTS `RoleClaim` (
    `RoleClaimId`  BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `RoleId`       BIGINT       NOT NULL,
    `ClaimType`    VARCHAR(30)  NOT NULL,
    `ClaimValue`   VARCHAR(30)  NOT NULL,
    `CreatedDate`  DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate` DATETIME,
    `DeletedDate`  DATETIME,
    `IsEdit`       BIT          NOT NULL DEFAULT 0,
    `IsDelete`     BIT          NOT NULL DEFAULT 0,
    CONSTRAINT `FK_RoleClaim_Role_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Role`(`RoleId`)
);