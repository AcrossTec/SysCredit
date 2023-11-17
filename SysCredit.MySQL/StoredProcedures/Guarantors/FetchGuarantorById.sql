DROP PROCEDURE IF EXISTS FetchGuarantorById;
DELIMITER //

CREATE PROCEDURE `FetchGuarantorById`(
    IN p_GuarantorId BIGINT
)
BEGIN
    SELECT *
    FROM `Guarantor`
    WHERE `IsDelete` = 0 AND `GuarantorId` = p_GuarantorId;
END //

DELIMITER ;