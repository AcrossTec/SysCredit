CREATE OR REPLACE PROCEDURE "public"."DeleteRelationship"
(
    relationship_id BIGINT
)
LANGUAGE plpgsql
AS $function$  
BEGIN
	
    UPDATE "public"."Relationship"
    SET
        "IsDelete"     = TRUE,
        "IsEdit"       = TRUE,
        "ModifiedDate" = CURRENT_TIMESTAMP,
        "DeletedDate"  = CURRENT_TIMESTAMP
    WHERE
        "RelationshipId" = relationship_id;
END;
$function$;