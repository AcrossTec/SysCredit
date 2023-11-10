DROP PROCEDURE IF EXISTS FetchLoanTypeByName;
DELIMITER //

CREATE PROCEDURE `FetchLoanTypeByName`(
    IN $Name VARCHAR(32)
)
BEGIN
    SELECT *
    FROM `LoanType` AS T
    WHERE T.`IsDelete` = 0 AND T.`Name` = $Name;
END //

DELIMITER ;