CREATE OR REPLACE FUNCTION "public"."FetchPaymentFrequencyByName"(Name TEXT)
RETURNS "public"."LoanType"
LANGUAGE plpgsql
AS $function$
DECLARE
	PaymentFrequency "PaymentFrequency";
BEGIN
	SELECT * INTO PaymentFrequency 
	FROM "PaymentFrequency"
	WHERE NOT "IsDelete" AND "Name" = Name;
	RETURN PaymentFrequency;
END;
$function$;