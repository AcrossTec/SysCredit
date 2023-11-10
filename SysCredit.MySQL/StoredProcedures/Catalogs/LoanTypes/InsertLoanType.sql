DROP PROCEDURE IF EXISTS InsertLoanType;
DELIMITER //

CREATE PROCEDURE `InsertLoanType`(
    IN $Name        VARCHAR(32),
    OUT $LoanTypeId BIGINT
)
BEGIN
    INSERT INTO `LoanType` (`Name`)
    VALUES ($Name);

    -- Retrieve the generated LoanTypeId and store it in the OUT parameter
    SET $LoanTypeId = LAST_INSERT_ID();
END //

DELIMITER ;