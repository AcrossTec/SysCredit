CREATE OR REPLACE PROCEDURE "public"."InsertRelationship"
(
    INOUT relationship_id BIGINT,
    IN    name            VARCHAR(32)
)
AS $$
BEGIN

    INSERT INTO "public"."Relationship"
        ("Name")
    VALUES
        (name)
    RETURNING 
        "RelationshipId" INTO relationship_id;
	
END;
$$ LANGUAGE PLPGSQL;