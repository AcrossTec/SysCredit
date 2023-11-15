CREATE OR REPLACE FUNCTION "public"."FetchLoanTypeById"
(
    loan_type_id BIGINT
)
RETURNS SETOF "LoanType"
LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT *
    FROM "LoanType"
    WHERE NOT "IsDelete" AND "LoanTypeId" = loan_type_id;
END;
$function$;