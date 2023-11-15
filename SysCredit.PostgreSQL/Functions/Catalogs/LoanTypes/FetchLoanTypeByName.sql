CREATE OR REPLACE FUNCTION "public"."FetchLoanTypeByName"
(
    name VARCHAR(32)
)
RETURNS SETOF "LoanType"
LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
    SELECT *
    FROM "LoanType"
    WHERE NOT "IsDelete" AND "Name" = name;
END;
$function$;