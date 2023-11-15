CREATE OR REPLACE FUNCTION "public"."FetchPaymentFrequencyByName"
( 
    name VARCHAR(32)
)
RETURNS SETOF "PaymentFrequency"
LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT * 
    FROM "PaymentFrequency"
    WHERE NOT "IsDelete" AND "Name" = name;
END;
$function$;