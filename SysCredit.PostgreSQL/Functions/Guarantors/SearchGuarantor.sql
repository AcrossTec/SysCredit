CREATE OR REPLACE FUNCTION "public"."SearchGuarantor"
(
    "Value"  TEXT,
    "Offset" INT DEFAULT NULL,
    "Limit"  INT DEFAULT NULL
)
RETURNS "public"."Guarantor"
AS $$
DECLARE
    Guarantor  "public"."Guarantor";
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
		
    IF ("Offset" IS NOT NULL) AND ("Limit" IS NOT NULL)
    THEN
        Statement = Statement || ' OFFSET $2 ROWS FETCH NEXT $3 ROWS ONLY';
        EXECUTE Statement INTO Guarantor USING "Value", "Offset", "Limit";
    ELSE
        EXECUTE Statement INTO Guarantor USING "Value";
    END IF;
	
    RETURN Guarantor;
END; 
$$ LANGUAGE plpgsql;