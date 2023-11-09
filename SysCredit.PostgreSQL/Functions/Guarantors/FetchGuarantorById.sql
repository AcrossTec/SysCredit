CREATE OR REPLACE FUNCTION "public"."FetchGuarantorById"
(
    GuarantorId BIGINT
)
RETURNS "public"."Guarantor"
AS $$
DECLARE
    Guarantor "public"."Guarantor";
BEGIN

    SELECT * INTO Guarantor
    FROM public."Guarantor"
    WHERE
        "IsDelete" = FALSE 
    AND
        "GuarantorId" = GuarantorId;

    Return Guarantor;

END; 
$$ LANGUAGE plpgsql;