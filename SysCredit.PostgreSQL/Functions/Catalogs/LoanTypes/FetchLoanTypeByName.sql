CREATE OR REPLACE FUNCTION "public"."FetchLoanTypeByName"
(
    name VARCHAR(32)
)
RETURNS "public"."LoanType"
LANGUAGE plpgsql
AS $function$
DECLARE
    loan_type "LoanType";
BEGIN
    SELECT * INTO loan_type 
    FROM "LoanType"
    WHERE NOT "IsDelete" AND "Name" = name;
    RETURN loan_type;
END;
$function$;