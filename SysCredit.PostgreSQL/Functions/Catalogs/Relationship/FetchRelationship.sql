CREATE OR REPLACE FUNCTION public."FetchRelationship"()
RETURNS SETOF "Relationship"
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT * 
    FROM "public"."Relationship"
    WHERE "IsDelete" = FALSE;
END;
$$;