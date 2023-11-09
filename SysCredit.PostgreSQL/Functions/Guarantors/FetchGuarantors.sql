CREATE OR REPLACE FUNCTION "public"."FetchGuarantors"()
RETURNS "public"."Guarantor"
AS $$
DECLARE
    Guarantor "public"."Guarantor";
BEGIN

    SELECT * INTO Guarantor
    FROM "public"."Guarantor"
    WHERE
        "IsDelete" = FALSE 
    ORDER BY "Name" ASC, "LastName" ASC;

    Return Guarantor;

END; 
$$ LANGUAGE plpgsql;