CREATE OR REPLACE FUNCTION "public"."UpdateRelationship"(RelationshipId bigint, Name TEXT)
RETURNS void
LANGUAGE plpgsql
AS $function$
BEGIN
    UPDATE "public"."Relationship"
    SET
        "Name" = Name,
        IsEdit = TRUE,
        ModifiedDate = CURRENT_TIMESTAMP
    WHERE
        "RelationshipId" = RelationshipId;
END;
$function$;