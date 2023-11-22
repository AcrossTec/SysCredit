DROP PROCEDURE IF EXISTS FetchLoansByCustomerId;
DELIMITER //

CREATE PROCEDURE `FetchLoansByCustomerId`(
    IN p_CustomerId BIGINT
)
BEGIN
    SELECT 
        L.*,
        LT.`Name` AS `LoanTypeName`,
        PF.`Name` AS `PaymentFrequencyName`
    FROM `Loan` AS L
    INNER JOIN `PaymentFrequency` AS PF ON PF.`PaymentFrequencyId` = L.`PaymentFrequencyId`
    INNER JOIN `LoanType`         AS LT ON LT.`LoanTypeId`         = L.`LoanTypeId`
    INNER JOIN `Customer`         AS C ON  L.`CustomerId`          = C.`CustomerId`
    WHERE C.`IsDelete` = 0 AND C.`CustomerId` = p_CustomerId;
END //

DELIMITER ;