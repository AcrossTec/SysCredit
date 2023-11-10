DROP PROCEDURE IF EXISTS UpdateLoanType;
DELIMITER //

CREATE PROCEDURE `UpdateLoanType`(
    IN $LoanTypeId BIGINT,
    IN $Name       VARCHAR(32)
)
BEGIN
    UPDATE `LoanType`
    SET
        `Name`         = $Name,
        `IsEdit`       = 1,
        `ModifiedDate` = CURRENT_TIMESTAMP
    WHERE
        `LoanTypeId` = $LoanTypeId;
END //

DELIMITER ;