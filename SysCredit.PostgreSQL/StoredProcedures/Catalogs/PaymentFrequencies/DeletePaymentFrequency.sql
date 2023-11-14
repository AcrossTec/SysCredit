CREATE OR REPLACE PROCEDURE "public"."DeletePaymentFrequency"
(
    payment_frequency_id BIGINT
)
LANGUAGE plpgsql
AS $function$  
BEGIN
	
    UPDATE "public"."PaymentFrequency"
    SET
        "IsDelete"     = TRUE,
        "IsEdit"       = TRUE,
        "ModifiedDate" = CURRENT_TIMESTAMP,
        "DeletedDate"  = CURRENT_TIMESTAMP
    WHERE
        "PaymentFrequencyId" = payment_frequency_id;
END;
$function$;