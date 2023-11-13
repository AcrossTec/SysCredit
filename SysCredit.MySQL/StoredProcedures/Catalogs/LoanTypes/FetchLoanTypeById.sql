DROP PROCEDURE IF EXISTS FetchLoanTypeById;
DELIMITER //

CREATE PROCEDURE `FetchLoanTypeById`(
	IN p_LoanTypeId BIGINT
)
BEGIN
    SELECT * FROM `LoanType` WHERE `IsDelete` = 0 AND `LoanTypeId` = p_LoanTypeId;
END //

DELIMITER ;