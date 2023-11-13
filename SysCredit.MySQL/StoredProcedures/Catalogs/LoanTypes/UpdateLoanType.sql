DROP PROCEDURE IF EXISTS UpdateLoanType;
DELIMITER //

CREATE PROCEDURE `UpdateLoanType`(
    IN p_LoanTypeId BIGINT,
    IN p_Name       VARCHAR(32)
)
BEGIN
    UPDATE `LoanType`
    SET
        `Name`         = p_Name,
        `IsEdit`       = 1,
        `ModifiedDate` = CURRENT_TIMESTAMP
    WHERE
        `LoanTypeId` = p_LoanTypeId;
END //

DELIMITER ;