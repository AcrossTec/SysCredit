CREATE OR REPLACE FUNCTION "public"."UpdateLoanType"(LoanTypeId bigint, Name TEXT)
RETURNS void
LANGUAGE plpgsql
AS $function$
BEGIN
    UPDATE "public"."LoanType"
    SET
        "Name" = Name,
        IsEdit = TRUE,
        ModifiedDate = CURRENT_TIMESTAMP
    WHERE
        "LoanTypeId" = LoanTypeId;
END;
$function$;