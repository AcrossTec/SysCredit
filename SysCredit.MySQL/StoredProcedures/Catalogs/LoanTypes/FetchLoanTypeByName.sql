DROP PROCEDURE IF EXISTS FetchLoanTypeByName;
DELIMITER //

CREATE PROCEDURE `FetchLoanTypeByName`(
	IN p_Name VARCHAR(32)
)
BEGIN
    SELECT *
    FROM `LoanType` AS T
    WHERE T.`IsDelete` = 0 AND T.`Name` = p_Name;
END //

DELIMITER ;