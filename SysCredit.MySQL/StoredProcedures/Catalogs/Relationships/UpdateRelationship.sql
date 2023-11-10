DROP PROCEDURE IF EXISTS UpdateRelationship;
DELIMITER //

CREATE PROCEDURE `UpdateRelationship`(
    IN $RelationshipId BIGINT,
    IN $Name VARCHAR(32)
)
BEGIN
    UPDATE `Relationship`
    SET
        `Name` = $Name,
        `IsEdit` = 1,
        `ModifiedDate` = CURRENT_TIMESTAMP
    WHERE
        `RelationshipId` = $RelationshipId;
END //

DELIMITER ;