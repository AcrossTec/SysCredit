-- Number: 6
CREATE TABLE IF NOT EXISTS `Relationship` (
    `RelationshipId` BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Name`           VARCHAR(32)  NOT NULL,
    `CreatedDate`    DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ModifiedDate`   DATETIME,
    `DeletedDate`    DATETIME,
    `IsEdit`         BIT          NOT NULL DEFAULT 0,
    `IsDelete`       BIT          NOT NULL DEFAULT 0,
    CONSTRAINT `AK_Relationship_Name` UNIQUE (`Name`)
);