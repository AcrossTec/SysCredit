CREATE OR REPLACE PROCEDURE "public"."DeleteGuarantor"
(
    IN GuarantorId BIGINT
)
LANGUAGE plpgsql
AS $$
BEGIN
	
    UPDATE public."Guarantor"
    SET
        "IsDelete"      = TRUE,
        "IsEdit"        = FALSE,
        "ModifiedDate"  = CURRENT_TIMESTAMP,
        "DeletedDate"   = CURRENT_TIMESTAMP
    WHERE
        "GuarantorId" = "GuarantorId";

END; $$