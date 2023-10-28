CREATE OR REPLACE FUNCTION "public"."FetchLoanType"()
RETURNS "Public"."LoanType"
LANGUAGE plpgsql
AS $function$
DECLARE
	LoanType "LoanType";
BEGIN
    SELECT * INTO LoanType
    FROM "public"."LoanType"
    WHERE "IsDelete" = FALSE;
	RETURN LoanType;
END;
$function$;