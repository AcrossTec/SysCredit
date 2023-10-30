-- Number: 16
CREATE TABLE IF NOT EXISTS `UserRole` (
    `RoleUserId`   BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `RoleId`       BIGINT       NOT NULL,
    `UserId`       BIGINT       NOT NULL,
    `CreatedDate`  DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate` DATETIME,
    `DeletedDate`  DATETIME,
    `IsEdit`       BIT          NOT NULL DEFAULT 0,
    `IsDelete`     BIT          NOT NULL DEFAULT 0,
    CONSTRAINT `FK_RoleUser_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `User`(`UserId`),
    CONSTRAINT `FK_RoleUser_Role_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Role`(`RoleId`)
);