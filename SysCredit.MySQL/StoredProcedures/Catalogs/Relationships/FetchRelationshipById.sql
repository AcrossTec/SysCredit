DROP PROCEDURE IF EXISTS FetchRelationshipById;
DELIMITER //

CREATE PROCEDURE `FetchRelationshipById`(
    IN $RelationshipId BIGINT
)
BEGIN
    SELECT * FROM `Relationship`
    WHERE `IsDelete` = 0 AND `RelationshipId` = $RelationshipId;
END //

DELIMITER ;