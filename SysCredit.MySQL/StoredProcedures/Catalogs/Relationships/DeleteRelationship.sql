DROP PROCEDURE IF EXISTS DeleteRelationship;
DELIMITER //

CREATE PROCEDURE `DeleteRelationship`(
    IN $RelationshipId BIGINT
)
BEGIN
    UPDATE `Relationship`
    SET
        `IsEdit`       = 1,
        `IsDelete`     = 1,
        `ModifiedDate` = CURRENT_TIMESTAMP,
        `DeletedDate`  = CURRENT_TIMESTAMP
    WHERE
        `RelationshipId` = $RelationshipId;
END //

DELIMITER ;