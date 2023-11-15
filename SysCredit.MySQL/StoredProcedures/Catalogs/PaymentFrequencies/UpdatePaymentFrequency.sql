DROP PROCEDURE IF EXISTS UpdatePaymentFrequency;
DELIMITER //

CREATE PROCEDURE `UpdatePaymentFrequency`(
    IN p_PaymentFrequencyId BIGINT,
    IN p_Name               VARCHAR(32)
)
BEGIN
    UPDATE `PaymentFrequency`
    SET
        `Name`         = p_Name,
        `IsEdit`       = 1,
        `ModifiedDate` = CURRENT_TIMESTAMP
    WHERE
        `PaymentFrequencyId` = p_PaymentFrequencyId;
END //

DELIMITER ;