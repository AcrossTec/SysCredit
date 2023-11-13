-- Number: 5
CREATE TABLE IF NOT EXISTS `Reference` (
    `ReferenceId`    BIGINT        NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Identification` VARCHAR(16),
    `Name`           VARCHAR(64)   NOT NULL,
    `LastName`       VARCHAR(64)   NOT NULL,
    `Gender`         BIT           NOT NULL,
    `Phone`          VARCHAR(16)   NOT NULL,
    `Email`          VARCHAR(64),
    `Address`        VARCHAR(256),
    `CreatedDate`    DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate`   DATETIME,
    `DeletedDate`    DATETIME,
    `IsEdit`         BIT           NOT NULL DEFAULT 0,
    `IsDelete`       BIT           NOT NULL DEFAULT 0
);