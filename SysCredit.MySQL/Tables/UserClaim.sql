-- Number: 15
CREATE TABLE IF NOT EXISTS `UserClaim` (
    `UserClaimId`  BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `UserId`       BIGINT       NOT NULL,
    `ClaimType`    VARCHAR(30)  NOT NULL,
    `ClaimValue`   VARCHAR(30)  NOT NULL,
    `CreatedDate`  DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate` DATETIME,
    `DeletedDate`  DATETIME,
    `IsEdit`       BIT          NOT NULL DEFAULT 0,
    `IsDelete`     BIT          NOT NULL DEFAULT 0,
    CONSTRAINT `FK_UserClaim_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `User`(`UserId`)
);