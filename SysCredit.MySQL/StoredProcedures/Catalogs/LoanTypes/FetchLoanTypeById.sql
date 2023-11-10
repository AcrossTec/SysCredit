DROP PROCEDURE IF EXISTS FetchLoanTypeById;
DELIMITER //

CREATE PROCEDURE `FetchLoanTypeById`(
    IN $LoanTypeId BIGINT
)
BEGIN
    SELECT * FROM `LoanType` WHERE `IsDelete` = 0 AND `LoanTypeId` = $LoanTypeId;
END //

DELIMITER ;