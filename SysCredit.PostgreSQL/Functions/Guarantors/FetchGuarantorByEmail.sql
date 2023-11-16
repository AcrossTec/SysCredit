CREATE OR REPLACE FUNCTION "public"."FetchGuarantorByEmail"
(
    email VARCHAR(64)
)
RETURNS SETOF "public"."Guarantor"
AS $$
BEGIN
	
    RETURN QUERY
    SELECT *
    FROM "public"."Guarantor"
    WHERE
        "IsDelete" = FALSE 
    AND
        "Email" = email;

END; 
$$ LANGUAGE plpgsql;