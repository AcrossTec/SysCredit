DROP PROCEDURE IF EXISTS FetchRelationship;
DELIMITER //

CREATE PROCEDURE `FetchRelationship`()
BEGIN
    SELECT * FROM `Relationship`
    WHERE `IsDelete` = 0;
END //

DELIMITER ;