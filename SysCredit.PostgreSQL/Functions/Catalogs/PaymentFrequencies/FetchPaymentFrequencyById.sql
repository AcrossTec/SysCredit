CREATE OR REPLACE FUNCTION "public"."FetchPaymentFrequencyById"
(
    payment_frequency_id BIGINT
)
RETURNS "public"."PaymentFrequency"
LANGUAGE plpgsql
AS $function$
DECLARE
    payment_frequency "PaymentFrequency";
BEGIN
    SELECT * INTO payment_frequency 
    FROM "PaymentFrequency"
    WHERE NOT "IsDelete" AND "PaymentFrequencyId" = payment_frequency_id;
    RETURN payment_frequency;
END;
$function$;
