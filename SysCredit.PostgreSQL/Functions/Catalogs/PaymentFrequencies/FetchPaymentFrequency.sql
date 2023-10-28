CREATE OR REPLACE FUNCTION "public"."FetchPaymentFrequency"()
RETURNS "Public"."PaymentFrequency"
LANGUAGE plpgsql
AS $function$
DECLARE
    PaymentFrequency "PaymentFrequency"
BEGIN
    SELECT * INTO PaymentFrequency
    FROM "public"."PaymentFrequency"
    WHERE IsDelete = FALSE;
    RETURN PaymentFrequency;
END;
$function$;