DROP PROCEDURE IF EXISTS UpdatePaymentFrequency;
DELIMITER //

CREATE PROCEDURE `UpdatePaymentFrequency`(
    IN $PaymentFrequencyId BIGINT,
    IN $Name               VARCHAR(32)
)
BEGIN
    UPDATE `PaymentFrequency`
    SET
        `Name`         = $Name,
        `IsEdit`       = 1,
        `ModifiedDate` = CURRENT_TIMESTAMP
    WHERE
        `PaymentFrequencyId` = $PaymentFrequencyId;
END //

DELIMITER ;