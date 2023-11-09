CREATE OR REPLACE FUNCTION "public"."FetchGuarantorByPhone"
(
    Phone VARCHAR(16)
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
        "Phone" = Phone;

    Return Guarantor;

END; 
$$ LANGUAGE plpgsql;