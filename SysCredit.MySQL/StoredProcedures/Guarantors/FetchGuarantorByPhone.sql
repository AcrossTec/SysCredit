DROP PROCEDURE IF EXISTS FetchGuarantorByPhone;
DELIMITER //

CREATE PROCEDURE `FetchGuarantorByPhone`(
    IN p_Phone VARCHAR(16)
)
BEGIN
    SELECT *
    FROM `Guarantor`
    WHERE `IsDelete` = 0 AND `Phone` = p_Phone;
END //

DELIMITER ;