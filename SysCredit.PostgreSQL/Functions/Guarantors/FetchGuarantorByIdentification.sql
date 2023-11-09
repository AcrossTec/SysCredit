CREATE OR REPLACE FUNCTION "public"."FetchGuarantorByIdentification"
(
    Identification VARCHAR(16)
)
RETURNS "public"."Guarantor"
AS $$
DECLARE
    Guarantor "public"."Guarantor";
BEGIN

    SELECT * INTO Guarantor
    FROM "public"."Guarantor"
    WHERE
        "IsDelete" = FALSE 
    AND
        "Identification" = Identification;

    Return Guarantor;

END; 
$$ LANGUAGE plpgsql;