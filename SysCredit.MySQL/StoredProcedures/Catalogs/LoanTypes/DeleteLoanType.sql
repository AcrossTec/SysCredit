DROP PROCEDURE IF EXISTS DeleteLoanType;
DELIMITER //

CREATE PROCEDURE `DeleteLoanType`(
	IN p_LoanTypeId BIGINT
)
BEGIN
    -- Perform the logical delete
    UPDATE `LoanType`
    SET
        `IsDelete`     = 1,                  -- Set IsDelete to 1 to indicate a logical delete
        `IsEdit`       = 1,                  -- Set IsEdit to 1 to indicate a logical delete
        `ModifiedDate` = CURRENT_TIMESTAMP,  -- Update ModifiedDate to the current timestamp
        `DeletedDate`  = CURRENT_TIMESTAMP   -- Set DeletedDate to the current timestamp
    WHERE
        `LoanTypeId` = p_LoanTypeId;
END //

DELIMITER ;