CREATE OR REPLACE FUNCTION "public"."InsertRelationship"(
	Name VARCHAR(32)

) RETURNS BIGINT AS $$
	DECLARE
	RelationshipId BIGINT;
	BEGIN
	INSERT INTO "public"."Relationship" ("Name")
	VALUES("Name")
	RETURNING "RelationshipId" INTO RelationshipId;

RETURN RelationshipId;
END;
$$ LANGUAGE PLPGSQL;