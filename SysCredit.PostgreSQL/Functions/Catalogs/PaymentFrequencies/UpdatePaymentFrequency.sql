CREATE OR REPLACE FUNCTION "public"."UpdatePaymentFrequency"(PaymentFrequencyId bigint, Name TEXT)
RETURNS void
LANGUAGE plpgsql
AS $function$
BEGIN
    UPDATE "public"."PaymentFrequency"
    SET
        "Name" = Name,
        IsEdit = TRUE,
        ModifiedDate = CURRENT_TIMESTAMP
    WHERE
        "PaymentFrequencyId" = PaymentFrequencyId;
END;
$function$;