DROP PROCEDURE IF EXISTS FetchPaymentFrequency;
DELIMITER //

CREATE PROCEDURE `FetchPaymentFrequency`()
BEGIN
    SELECT * FROM `PaymentFrequency`
    WHERE `IsDelete` = 0;
END //

DELIMITER ;