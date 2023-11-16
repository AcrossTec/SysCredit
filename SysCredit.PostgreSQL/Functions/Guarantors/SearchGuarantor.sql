CREATE OR REPLACE FUNCTION "public"."SearchGuarantor"
(
    "value"  TEXT,
    "offset" INT DEFAULT NULL,
    "limit"  INT DEFAULT NULL
)
RETURNS SETOF "public"."Guarantor"
AS $$
DECLARE
    Statement  TEXT;
BEGIN
    Statement = '
        SELECT *
        FROM "public"."Guarantor"
        WHERE NOT "IsDelete"
          AND ("Identification" LIKE CONCAT(''%'', COALESCE($1, "Identification"), ''%'')
           OR "Name"            LIKE CONCAT(''%'', COALESCE($1, "Name"          ), ''%'')
           OR "LastName"        LIKE CONCAT(''%'', COALESCE($1, "LastName"      ), ''%'')
           OR "Phone"           LIKE CONCAT(''%'', COALESCE($1, "Phone"         ), ''%'')
           OR "Email"           LIKE CONCAT(''%'', COALESCE($1, "Email"         ), ''%''))
 		ORDER BY "Name" ASC, "LastName" ASC';

    IF ("offset" IS NOT NULL) AND ("limit" IS NOT NULL)
    THEN
        Statement = Statement || ' OFFSET $2 ROWS FETCH NEXT $3 ROWS ONLY';
        RETURN QUERY EXECUTE Statement USING "value", "offset", "limit";
    ELSE
        RETURN QUERY EXECUTE Statement USING "value";
    END IF;
	
END; 
$$ LANGUAGE plpgsql;