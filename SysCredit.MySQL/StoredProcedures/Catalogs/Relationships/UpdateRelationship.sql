DROP PROCEDURE IF EXISTS UpdateRelationship;
DELIMITER //

CREATE PROCEDURE `UpdateRelationship`(
    IN p_RelationshipId BIGINT,
    IN p_Name VARCHAR(32)
)
BEGIN
    UPDATE `Relationship`
    SET
        `Name` = p_Name,
        `IsEdit` = 1,
        `ModifiedDate` = CURRENT_TIMESTAMP
    WHERE
        `RelationshipId` = p_RelationshipId;
END //

DELIMITER ;