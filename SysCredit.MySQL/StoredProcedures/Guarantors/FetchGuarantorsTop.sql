DROP PROCEDURE IF EXISTS FetchGuarantorsTop;
DELIMITER //

CREATE PROCEDURE `FetchGuarantorsTop`(
    IN p_Offset INT,
    IN p_Limit  INT
)
BEGIN
    SELECT *
    FROM `Guarantor`
    WHERE `IsDelete` = 0
    ORDER BY `Name` ASC, `LastName` ASC
    LIMIT p_Offset, p_Limit;
END //

DELIMITER ;