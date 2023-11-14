CREATE OR REPLACE PROCEDURE "public"."UpdatePaymentFrequency"
(
    payment_frequency_id BIGINT, 
    name                 VARCHAR(32)
)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE "public"."PaymentFrequency"
    SET
        "Name"         = name,
        "IsEdit"       = TRUE,
        "ModifiedDate" = CURRENT_TIMESTAMP
    WHERE
        "PaymentFrequencyId" = payment_frequency_id;
END;
$$;