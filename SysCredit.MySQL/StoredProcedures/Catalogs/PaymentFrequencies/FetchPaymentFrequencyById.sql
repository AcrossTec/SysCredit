DROP PROCEDURE IF EXISTS FetchPaymentFrequencyById;
DELIMITER //

CREATE PROCEDURE `FetchPaymentFrequencyById`(
    IN $PaymentFrequencyId BIGINT
)
BEGIN
    SELECT * FROM `PaymentFrequency`
    WHERE `IsDelete` = 0 AND `PaymentFrequencyId` = $PaymentFrequencyId;
END //

DELIMITER ;