CREATE OR REPLACE FUNCTION "public"."FetchGuarantorByEmail"
(
    Email VARCHAR(64)
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
        "Email" = Email;

    Return Guarantor;

END; 
$$ LANGUAGE plpgsql;