DROP FUNCTION IF EXISTS VerifyLoanTypeReference;
DELIMITER //

CREATE FUNCTION VerifyLoanTypeReference(p_LoanTypeId BIGINT) RETURNS BIT DETERMINISTIC READS SQL DATA
BEGIN
    DECLARE v_Result BOOLEAN;

    -- Verifica si existe una referencia a LoanTypeId en la tabla Loan
    IF (SELECT COUNT(*) FROM `Loan` WHERE `LoanTypeId` = p_LoanTypeId) > 0 THEN
        SET v_Result = 1;
    ELSE
        SET v_Result = 0;
    END IF;

    -- Devuelve el resultado
    RETURN v_Result;
END //

DELIMITER ;