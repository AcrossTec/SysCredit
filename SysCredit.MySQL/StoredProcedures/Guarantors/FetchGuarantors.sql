DROP PROCEDURE IF EXISTS FetchGuarantors;
DELIMITER //

CREATE PROCEDURE `FetchGuarantors`()
BEGIN
    SELECT *
    FROM `Guarantor`
    WHERE `IsDelete` = 0
    ORDER BY `Name` ASC, `LastName` ASC;
END //

DELIMITER ;