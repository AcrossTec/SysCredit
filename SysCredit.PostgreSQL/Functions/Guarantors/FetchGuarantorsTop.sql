CREATE OR REPLACE FUNCTION "public"."FetchGuarantorsTop"
(
    "Offset" INT,
    "Limit"  INT
)
RETURNS "public"."Guarantor"
AS $$
DECLARE
    Guarantor "public"."Guarantor";
BEGIN

    SELECT * INTO Guarantor
    FROM "public"."Guarantor"
    WHERE "IsDelete" = FALSE
    ORDER BY "Name" ASC, "LastName" ASC
    OFFSET "Offset" ROWS FETCH NEXT "Limit" ROWS ONLY;

    Return Guarantor;

END; 
$$ LANGUAGE plpgsql;