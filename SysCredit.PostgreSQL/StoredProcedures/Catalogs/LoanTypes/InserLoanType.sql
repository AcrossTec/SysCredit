CREATE OR REPLACE PROCEDURE "public"."InsertLoanType"
(
    INOUT loan_type_id BIGINT,
    IN    name         VARCHAR(32)
)
AS $$
BEGIN

    INSERT INTO "public"."LoanType"
        ("Name")
    VALUES
        (name)
    RETURNING 
        "LoanTypeId" INTO loan_type_id;
	
END;
$$ LANGUAGE PLPGSQL;