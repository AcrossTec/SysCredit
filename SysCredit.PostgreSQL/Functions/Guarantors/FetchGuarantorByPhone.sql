CREATE OR REPLACE FUNCTION "public"."FetchGuarantorByPhone"
(
    phone VARCHAR(16)
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
        "Phone" = Phone;

END; 
$$ LANGUAGE plpgsql;