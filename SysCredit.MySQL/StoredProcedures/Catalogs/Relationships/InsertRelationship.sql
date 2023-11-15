DROP PROCEDURE IF EXISTS InsertRelationship;
DELIMITER //

CREATE PROCEDURE `InsertRelationship`(
    IN  p_Name           VARCHAR(32),
    OUT p_RelationShipId BIGINT
)
BEGIN
    INSERT INTO `Relationship` (`Name`)
    VALUES (p_Name);
    
    -- Retrieve the generated RelationShipId and store it in the OUT parameter
    SET p_RelationShipId = LAST_INSERT_ID();
END //

DELIMITER ;