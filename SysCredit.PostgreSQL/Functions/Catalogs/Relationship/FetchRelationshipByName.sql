CREATE OR REPLACE FUNCTION "public"."FetchRelationshipByName"
(
    name VARCHAR(32)
)
RETURNS "public"."Relationship"
LANGUAGE plpgsql
AS $function$
DECLARE
    relationship "Relationship";
BEGIN
    SELECT * INTO relationship 
    FROM "Relationship"
    WHERE NOT "IsDelete" AND "Name" = name;
    RETURN relationship;
END;
$function$;