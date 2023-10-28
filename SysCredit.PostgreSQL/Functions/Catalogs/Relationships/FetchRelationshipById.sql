CREATE OR REPLACE FUNCTION "public"."FetchRelationshipById"(RelationshipId BIGINT)
RETURNS "public"."Relationship"
LANGUAGE plpgsql
AS $function$
DECLARE
	Relationship "Relationship";
BEGIN
	SELECT * INTO Relationship FROM "Relationship"
	WHERE NOT "IsDelete" AND "RelationshipId" = RelationshipId;
	RETURN Relationship;
END;
$function$;
