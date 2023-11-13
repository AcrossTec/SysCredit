DROP PROCEDURE IF EXISTS InsertLoanType;
DELIMITER //

CREATE PROCEDURE `InsertLoanType`(
    IN  p_Name       VARCHAR(32),
    OUT p_LoanTypeId BIGINT
)
BEGIN
    INSERT INTO `LoanType` (`Name`)
    VALUES (p_Name);

    -- Retrieve the generated LoanTypeId and store it in the OUT parameter
    SET p_LoanTypeId = LAST_INSERT_ID();
END //

DELIMITER ;