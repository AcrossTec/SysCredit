DROP PROCEDURE IF EXISTS FetchGuarantorByIdentification;
DELIMITER //

CREATE PROCEDURE `FetchGuarantorByIdentification`(
    IN p_Identification VARCHAR(16)
)
BEGIN
    SELECT *
    FROM `Guarantor`
    WHERE `IsDelete` = 0 AND `Identification` = p_Identification;
END //

DELIMITER ;