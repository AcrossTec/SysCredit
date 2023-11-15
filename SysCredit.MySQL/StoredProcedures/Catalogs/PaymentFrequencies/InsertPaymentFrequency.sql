DROP PROCEDURE IF EXISTS InsertPaymentFrequency;
DELIMITER //

CREATE PROCEDURE `InsertPaymentFrequency`(
    IN  p_Name               VARCHAR(32),
    OUT p_PaymentFrequencyId BIGINT
)
BEGIN
    INSERT INTO `PaymentFrequency` (`Name`)
    VALUES (p_Name);

    -- Recuperar el PaymentFrequencyId generado y almacenarlo en el parámetro de salida
    SET p_PaymentFrequencyId = LAST_INSERT_ID();
END //

DELIMITER ;