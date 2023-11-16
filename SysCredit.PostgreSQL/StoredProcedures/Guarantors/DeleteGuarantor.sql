CREATE OR REPLACE PROCEDURE "public"."DeleteGuarantor"
(
    IN guarantor_id BIGINT
)
LANGUAGE plpgsql
AS $$
BEGIN

    UPDATE public."Guarantor"
    SET
        "IsDelete"      = TRUE,
        "IsEdit"        = TRUE,
        "ModifiedDate"  = CURRENT_TIMESTAMP,
        "DeletedDate"   = CURRENT_TIMESTAMP
    WHERE
        "GuarantorId" = guarantor_id;

END; $$