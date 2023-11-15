DROP PROCEDURE IF EXISTS FetchRelationshipByName;
DELIMITER //

CREATE PROCEDURE `FetchRelationshipByName`(
    IN p_Name VARCHAR(32)
)
BEGIN
    SELECT R.*
    FROM `Relationship` AS R
    WHERE R.`IsDelete` = 0 AND R.`Name` = p_Name;
END //

DELIMITER ;