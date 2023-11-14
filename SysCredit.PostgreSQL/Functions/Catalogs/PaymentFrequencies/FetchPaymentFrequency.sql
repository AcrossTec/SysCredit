CREATE OR REPLACE FUNCTION public."FetchPaymentFrequency"()
RETURNS SETOF "PaymentFrequency"
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT * 
    FROM "public"."PaymentFrequency"
    WHERE "IsDelete" = FALSE;
END;
$$;