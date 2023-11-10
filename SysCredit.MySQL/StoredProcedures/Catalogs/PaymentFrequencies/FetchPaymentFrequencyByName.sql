DROP PROCEDURE IF EXISTS FetchPaymentFrequencyByName;
DELIMITER //

CREATE PROCEDURE `FetchPaymentFrequencyByName`(
    IN $Name VARCHAR(32)
)
BEGIN
    SELECT * FROM `PaymentFrequency` AS PF
    WHERE PF.`IsDelete` = 0 AND PF.`Name` = $Name;
END //

DELIMITER ;