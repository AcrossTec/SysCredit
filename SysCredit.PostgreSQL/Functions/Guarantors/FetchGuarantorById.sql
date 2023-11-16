CREATE OR REPLACE FUNCTION "public"."FetchGuarantorById"
(
    guarantor_id BIGINT
)
RETURNS SETOF "public"."Guarantor"
AS $$
BEGIN

    RETURN QUERY
    SELECT *
    FROM "public"."Guarantor"
    WHERE
        "IsDelete" = FALSE 
    AND
        "GuarantorId" = guarantor_id;

END; 
$$ LANGUAGE plpgsql;