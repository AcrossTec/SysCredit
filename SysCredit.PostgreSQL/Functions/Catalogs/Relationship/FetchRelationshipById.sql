CREATE OR REPLACE FUNCTION "public"."FetchRelationshipById"
(
    relationship_id BIGINT
)
RETURNS "public"."Relationship"
LANGUAGE plpgsql
AS $function$
DECLARE
    relationship "Relationship";
BEGIN
    SELECT * INTO relationship 
    FROM "Relationship"
    WHERE NOT "IsDelete" AND "RelationshipId" = relationship_id;
    RETURN relationship;
END;
$function$;