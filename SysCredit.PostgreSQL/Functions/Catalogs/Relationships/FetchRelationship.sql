CREATE OR REPLACE FUNCTION "public"."FetchRelationship"()
RETURNS "public"."Relationship"
LANGUAGE plpgsql
AS $function$
DECLARE
    Relationship "Relationship";
BEGIN
    SELECT * INTO Relationship
    FROM "public"."Relationship"
    WHERE IsDelete = FALSE;
    RETURN Relationship;
END;
$function$;