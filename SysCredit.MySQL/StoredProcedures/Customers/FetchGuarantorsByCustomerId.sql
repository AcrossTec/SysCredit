DROP PROCEDURE IF EXISTS FetchGuarantorsByCustomerId;
DELIMITER //

CREATE PROCEDURE `FetchGuarantorsByCustomerId`(
    IN p_CustomerId BIGINT
)
BEGIN
    SELECT G.*
    FROM `Guarantor` AS G
    INNER JOIN `CustomerGuarantor` AS CG ON G.`GuarantorId` = CG.`GuarantorId`
    WHERE G.`IsDelete` = 0 
    AND CG.`CustomerId` = p_CustomerId;
END //

DELIMITER ;