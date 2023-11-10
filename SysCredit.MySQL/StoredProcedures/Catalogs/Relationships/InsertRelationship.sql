DROP PROCEDURE IF EXISTS InsertRelationship;
DELIMITER //

CREATE PROCEDURE `InsertRelationship`(
    IN  $Name           VARCHAR(32),
    OUT $RelationShipId BIGINT
)
BEGIN
    INSERT INTO `Relationship` (`Name`)
    VALUES ($Name);
    
    -- Retrieve the generated RelationShipId and store it in the OUT parameter
    SET $RelationShipId = LAST_INSERT_ID();
END //

DELIMITER ;