DROP PROCEDURE IF EXISTS FetchCustomerByGuarantorId;
DELIMITER //

CREATE PROCEDURE `FetchCustomerByGuarantorId`(
    IN p_GuarantorId BIGINT
)
BEGIN
    SELECT C.*
    FROM Customer AS C
    INNER JOIN CustomerGuarantor AS CG ON C.CustomerId = CG.CustomerId
    WHERE C.IsDelete = 0 
    AND CG.GuarantorId = p_GuarantorId;
END //

DELIMITER ;