CREATE OR REPLACE FUNCTION "public"."FetchRelationshipByName"(Name TEXT)
RETURNS "public"."Relationship"
LANGUAGE plpgsql
AS $function$
DECLARE
	Relationship "Relationship";
BEGIN
	SELECT * INTO Relationship 
	FROM "Relationship"
	WHERE NOT "IsDelete" AND "Name" = Name;
	RETURN Relationship;
END;
$function$;