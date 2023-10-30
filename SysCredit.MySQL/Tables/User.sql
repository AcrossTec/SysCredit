-- Number: 8
CREATE TABLE IF NOT EXISTS `User` (
    `UserId`       BIGINT        NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `UserName`     VARCHAR(64)   NOT NULL,
    `Email`        VARCHAR(64)   NOT NULL,
    `Password`     TEXT          NOT NULL,
    `Phone`        VARCHAR(16)   NOT NULL,
    `CreatedDate`  DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate` DATETIME,
    `DeletedDate`  DATETIME,
    `IsEdit`       BIT           NOT NULL DEFAULT 0,
    `IsDelete`     BIT           NOT NULL DEFAULT 0,
    CONSTRAINT `AK_User_UserName` UNIQUE (`UserName`),
    CONSTRAINT `AK_User_Email`    UNIQUE (`Email`),
    CONSTRAINT `AK_User_Phone`    UNIQUE (`Phone`)
);