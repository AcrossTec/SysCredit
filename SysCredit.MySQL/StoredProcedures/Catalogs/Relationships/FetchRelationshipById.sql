DROP PROCEDURE IF EXISTS FetchRelationshipById;
DELIMITER //

CREATE PROCEDURE `FetchRelationshipById`(
    IN p_RelationshipId BIGINT
)
BEGIN
    SELECT * FROM `Relationship`
    WHERE `IsDelete` = 0 AND `RelationshipId` = p_RelationshipId;
END //

DELIMITER ;