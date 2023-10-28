CREATE OR REPLACE FUNCTION "public"."FetchLoanTypeByName"(Name TEXT)
RETURNS "public"."LoanType"
LANGUAGE plpgsql
AS $function$
DECLARE
	LoanType "LoanType";
BEGIN
	SELECT * INTO LoanType 
	FROM "LoanType"
	WHERE NOT "IsDelete" AND "Name" = Name;
	RETURN LoanType;
END;
$function$;