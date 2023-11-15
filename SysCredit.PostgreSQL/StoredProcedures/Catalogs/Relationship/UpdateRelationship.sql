CREATE OR REPLACE PROCEDURE "public"."UpdateRelationship"
(
    relationship_id BIGINT, 
    name            VARCHAR(32)
)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE "public"."Relationship"
    SET
        "Name"         = name,
        "IsEdit"       = TRUE,
        "ModifiedDate" = CURRENT_TIMESTAMP
    WHERE
        "RelationshipId" = relationship_id;
END;
$$;