CREATE OR REPLACE FUNCTION "public"."InsertLoanType"(
	Name VARCHAR(32)

) RETURNS BIGINT AS $$
	DECLARE
	LoanTypeId BIGINT;
	BEGIN
	INSERT INTO "public"."LoanType" ("Name")
	VALUES("Name")
	RETURNING "LoanTypeId" INTO LoanTypeId;

RETURN LoanTypeId;
END;
$$ LANGUAGE PLPGSQL;