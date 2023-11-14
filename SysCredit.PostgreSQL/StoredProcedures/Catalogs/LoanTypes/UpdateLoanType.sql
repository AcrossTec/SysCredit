CREATE OR REPLACE PROCEDURE "public"."UpdateLoanType"
(
    loan_type_id BIGINT, 
    name         VARCHAR(32)
)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE "public"."LoanType"
    SET
        "Name"         = name,
        "IsEdit"       = TRUE,
        "ModifiedDate" = CURRENT_TIMESTAMP
    WHERE
        "LoanTypeId" = loan_type_id;
END;
$$;