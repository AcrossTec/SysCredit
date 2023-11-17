DROP PROCEDURE IF EXISTS FetchGuarantorByEmail;
DELIMITER //

CREATE PROCEDURE `FetchGuarantorByEmail`(
    IN p_Email VARCHAR(64)
)
BEGIN
    SELECT *
    FROM `Guarantor`
    WHERE `IsDelete` = 0 AND `Email` = p_Email;
END //

DELIMITER ;