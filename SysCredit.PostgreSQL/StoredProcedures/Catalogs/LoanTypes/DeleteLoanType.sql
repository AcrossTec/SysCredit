CREATE OR REPLACE PROCEDURE "public"."DeleteLoanType"
(
    loan_type_id BIGINT
)
LANGUAGE plpgsql
AS $function$  
BEGIN
	
    UPDATE "public"."LoanType"
    SET
        "IsDelete"     = TRUE,
        "IsEdit"       = TRUE,
        "ModifiedDate" = CURRENT_TIMESTAMP,
        "DeletedDate"  = CURRENT_TIMESTAMP
    WHERE
        "LoanTypeId" = loan_type_id;
END;
$function$;