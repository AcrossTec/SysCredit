CREATE OR REPLACE PROCEDURE "public"."InsertPaymentFrequency"
(
    INOUT payment_frequency_id BIGINT,
    IN    name                 VARCHAR(32)
)
AS $$
BEGIN

    INSERT INTO "public"."PaymentFrequency"
        ("Name")
    VALUES
        (name)
    RETURNING 
        "PaymentFrequencyId" INTO payment_frequency_id;
	
END;
$$ LANGUAGE PLPGSQL;