DROP PROCEDURE IF EXISTS FetchCustomerByGuarantorId;
DELIMITER //

CREATE PROCEDURE `FetchCustomerByGuarantorId`(
    IN p_GuarantorId BIGINT
)
BEGIN
    SELECT *
    FROM `CustomerGuarantor`
    WHERE `IsDelete` = 0 AND `GuarantorId` = p_GuarantorId;
END //

DELIMITER ;