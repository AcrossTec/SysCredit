DROP PROCEDURE IF EXISTS InsertPaymentFrequency;
DELIMITER //

CREATE PROCEDURE `InsertPaymentFrequency`(
    IN $Name                VARCHAR(32),
    OUT $PaymentFrequencyId BIGINT
)
BEGIN
    INSERT INTO `PaymentFrequency` (`Name`)
    VALUES ($Name);

    -- Recuperar el PaymentFrequencyId generado y almacenarlo en el par�metro de salida
    SET $PaymentFrequencyId = LAST_INSERT_ID();
END //

DELIMITER ;