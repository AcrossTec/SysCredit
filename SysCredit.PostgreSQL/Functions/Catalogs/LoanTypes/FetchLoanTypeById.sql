CREATE OR REPLACE FUNCTION "public"."FetchLoanTypeById"(LoanTypeId BIGINT)
RETURNS "public"."LoanType"
LANGUAGE plpgsql
AS $function$
DECLARE
	LoanType "LoanType";
BEGIN
	SELECT * INTO LoanType 
	FROM "LoanType"
	WHERE NOT "IsDelete" AND "LoanTypeId" = LoanTypeId;
	RETURN LoanType;
END;
$function$;
