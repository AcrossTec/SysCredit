CREATE OR REPLACE FUNCTION "public"."FetchPaymentFrequencyById"(PaymentFrequencyId BIGINT)
RETURNS "public"."PaymentFrequency"
LANGUAGE plpgsql
AS $function$
DECLARE
	PaymentFrequency "PaymentFrequency";
BEGIN
	SELECT * INTO PaymentFrequency FROM "PaymentFrequency"
	WHERE NOT "IsDelete" AND "PaymentFrequencyId" = PaymentFrequencyId;
	RETURN PaymentFrequency;
END;
$function$;
