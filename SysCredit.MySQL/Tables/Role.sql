-- Number: 7
CREATE TABLE IF NOT EXISTS `Role` (
    `RoleId`       BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Name`         VARCHAR(30)  NOT NULL,
    `CreatedDate`  DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate` DATETIME,
    `DeletedDate`  DATETIME,
    `IsEdit`       BIT          NOT NULL DEFAULT 0,
    `IsDelete`     BIT          NOT NULL DEFAULT 0
);