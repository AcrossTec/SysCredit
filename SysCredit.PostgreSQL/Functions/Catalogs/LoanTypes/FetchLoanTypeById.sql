CREATE OR REPLACE FUNCTION "public"."FetchLoanTypeById"
(
    loan_type_id BIGINT
)
RETURNS "public"."LoanType"
LANGUAGE plpgsql
AS $function$
DECLARE
    loan_type "LoanType";
BEGIN
    SELECT * INTO loan_type 
    FROM "LoanType"
    WHERE NOT "IsDelete" AND "LoanTypeId" = loan_type_id;
    RETURN loan_type;
END;
$function$;