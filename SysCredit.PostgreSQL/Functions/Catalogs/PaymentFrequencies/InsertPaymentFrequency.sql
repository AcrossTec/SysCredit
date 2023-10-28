CREATE OR REPLACE FUNCTION "public"."InsertPaymentFrequency"(
	Name VARCHAR(32)

) RETURNS BIGINT AS $$
	DECLARE
	PaymentFrequencyId BIGINT;
BEGIN
	INSERT INTO "public"."PaymentFrequency" ("Name")
	VALUES("Name")
	RETURNING "PaymentFrequencyId" INTO PaymentFrequencyId;

RETURN PaymentFrequencyId;
END;
$$ LANGUAGE PLPGSQL;