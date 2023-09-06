/*
	THIS PROCEDURE WILL VERIFY IF LOAN TABLE HAS A REFERENCE TO LoanTypeId
*/
CREATE FUNCTION [dbo].[VerifyLoanTypeReference] (@LoanTypeId BIGINT)
RETURNS BIT
AS
BEGIN
    DECLARE @Result BIT
	IF EXISTS (SELECT * FROM [Loan] WHERE [LoanTypeId] = @LoanTypeId)
        SET @Result = 1; -- Si existe, asigna 1 a @Result (VERDADERO)
    ELSE
        SET @Result = 0; -- Si no existe, asigna 0 a @Result (FALSO)

    -- Devuelve el valor almacenado en la variable @Result
    RETURN @Result;
END
GO