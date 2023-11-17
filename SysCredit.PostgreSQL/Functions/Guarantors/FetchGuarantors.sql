CREATE OR REPLACE FUNCTION "public"."FetchGuarantors"()
RETURNS SETOF "public"."Guarantor"
AS $$
BEGIN

    RETURN QUERY
    SELECT *
    FROM "public"."Guarantor"
    WHERE
        "IsDelete" = FALSE 
    ORDER BY "Name" ASC, "LastName" ASC;

END; 
$$ LANGUAGE plpgsql;