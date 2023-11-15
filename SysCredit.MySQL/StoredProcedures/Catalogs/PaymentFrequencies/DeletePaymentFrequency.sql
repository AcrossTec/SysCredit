DROP PROCEDURE IF EXISTS DeletePaymentFrequency;
DELIMITER //

CREATE PROCEDURE `DeletePaymentFrequency`(
    IN p_PaymentFrequencyId BIGINT
)
BEGIN
    -- Realizar la eliminación lógica
    UPDATE `PaymentFrequency`
    SET
        `IsDelete`      = 1,
        `IsEdit`        = 1,
        `ModifiedDate`  = CURRENT_TIMESTAMP(),
        `DeletedDate`   = CURRENT_TIMESTAMP()
    WHERE
        `PaymentFrequencyId` = p_PaymentFrequencyId;
END //

DELIMITER ;