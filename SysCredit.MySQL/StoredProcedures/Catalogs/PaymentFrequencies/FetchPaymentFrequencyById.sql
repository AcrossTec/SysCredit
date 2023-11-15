DROP PROCEDURE IF EXISTS FetchPaymentFrequencyById;
DELIMITER //

CREATE PROCEDURE `FetchPaymentFrequencyById`(
    IN p_PaymentFrequencyId BIGINT
)
BEGIN
    SELECT * FROM `PaymentFrequency`
    WHERE `IsDelete` = 0 AND `PaymentFrequencyId` = p_PaymentFrequencyId;
END //

DELIMITER ;