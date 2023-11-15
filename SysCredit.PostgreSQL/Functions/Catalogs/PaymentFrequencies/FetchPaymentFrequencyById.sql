CREATE OR REPLACE FUNCTION "public"."FetchPaymentFrequencyById"
(
    payment_frequency_id BIGINT
)
RETURNS SETOF "PaymentFrequency"
LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT *
    FROM "PaymentFrequency"
    WHERE NOT "IsDelete" AND "PaymentFrequencyId" = payment_frequency_id;
END;
$function$;
