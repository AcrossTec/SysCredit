CREATE OR REPLACE FUNCTION "public"."FetchGuarantorByIdentification"
(
    identification VARCHAR(16)
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
        "Identification" = identification;

END; 
$$ LANGUAGE plpgsql;