CREATE OR REPLACE FUNCTION "public"."VerifyLoanTypeReference"
(
    loan_type_id BIGINT
)
RETURNS BIT
LANGUAGE plpgsql
AS $$
BEGIN
    IF EXISTS (SELECT * FROM "Loan" WHERE "LoanTypeId" = loan_type_id)
	THEN
	    RETURN B'1'; -- Si existe, asigna 1 a @Result (VERDADERO)
	ELSE
	    RETURN B'0'; -- Si no existe, asigna 0 a @Result (FALSO)
	END IF;
END;
$$;