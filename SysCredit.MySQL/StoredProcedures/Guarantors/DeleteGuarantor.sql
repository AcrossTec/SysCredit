DROP PROCEDURE IF EXISTS DeleteGuarantor;
DELIMITER //

CREATE PROCEDURE `DeleteGuarantor`(
    IN p_GuarantorId BIGINT
)
BEGIN
    UPDATE `Guarantor`
    SET
        `IsDelete`      = 1,
        `IsEdit`        = 1,
        `ModifiedDate`  = CURRENT_TIMESTAMP,
        `DeletedDate`   = CURRENT_TIMESTAMP
    WHERE
        `GuarantorId` = p_GuarantorId;
END //

DELIMITER ;