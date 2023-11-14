CREATE OR REPLACE FUNCTION "public"."FetchPaymentFrequencyByName"
(
    name VARCHAR(32)
)
RETURNS "public"."PaymentFrequency"
LANGUAGE plpgsql
AS $function$
DECLARE
    payment_frequency "PaymentFrequency";
BEGIN
    SELECT * INTO payment_frequency 
    FROM "PaymentFrequency"
    WHERE NOT "IsDelete" AND "Name" = name;
    RETURN payment_frequency;
END;
$function$;