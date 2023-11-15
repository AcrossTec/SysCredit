CREATE OR REPLACE FUNCTION "public"."FetchRelationshipByName"
(
    name VARCHAR(32)
)
RETURNS SETOF "Relationship"
LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT * 
    FROM "Relationship"
    WHERE NOT "IsDelete" AND "Name" = name;
END;
$function$;