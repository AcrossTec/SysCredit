CREATE OR REPLACE FUNCTION "public"."FetchRelationshipById"
(
    relationship_id BIGINT
)
RETURNS SETOF "Relationship"
LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT *
    FROM "Relationship"
    WHERE NOT "IsDelete" AND "RelationshipId" = relationship_id;
END;
$function$;