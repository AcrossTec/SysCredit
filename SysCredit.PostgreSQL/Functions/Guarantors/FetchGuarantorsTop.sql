CREATE OR REPLACE FUNCTION "public"."FetchGuarantorsTop"
(
    "offset" INT, 
    "limit"   INT
)
RETURNS SETOF "public"."Guarantor"
AS $$
BEGIN

    RETURN QUERY
    SELECT *
    FROM "public"."Guarantor"
    WHERE "IsDelete" = FALSE
    ORDER BY "Name" ASC, "LastName" ASC
    OFFSET "offset" ROWS FETCH NEXT "limit" ROWS ONLY;

END; 
$$ LANGUAGE plpgsql;