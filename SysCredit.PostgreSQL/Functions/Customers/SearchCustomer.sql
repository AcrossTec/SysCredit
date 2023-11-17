CREATE OR REPLACE FUNCTION "public"."SearchCustomer" (
    "value"  TEXT,
    "offset" INT DEFAULT NULL,
    "limit"  INT DEFAULT NULL
)
RETURNS SETOF "SearchCustomerInfo"
LANGUAGE plpgsql
AS $$
DECLARE
    Statement TEXT;
BEGIN
    Statement = '
        SELECT
            C.*,
            "public"."GetJsonReferencesByCustomerId"(C."CustomerId") AS "JsonReferences",
            "public"."GetJsonGuarantorsByCustomerId"(C."CustomerId") AS "JsonGuarantors"
        FROM "public"."Customer" AS C
        WHERE C."IsDelete" = FALSE
          AND (C."Identification" LIKE CONCAT(''%'', COALESCE($1, C."Identification"), ''%'')
           OR C."Name"            LIKE CONCAT(''%'', COALESCE($1, C."Name"          ), ''%'')
           OR C."LastName"        LIKE CONCAT(''%'', COALESCE($1, C."LastName"      ), ''%'')
           OR C."Phone"           LIKE CONCAT(''%'', COALESCE($1, C."Phone"         ), ''%'')
           OR C."Email"           LIKE CONCAT(''%'', COALESCE($1, C."Email"         ), ''%''))
        ORDER BY C."Name" ASC, C."LastName" ASC';

    IF ("offset" IS NOT NULL) AND ("limit" IS NOT NULL) 
    THEN
        Statement = Statement || 'OFFSET $2 ROWS FETCH NEXT $3 ROWS ONLY';
        RETURN QUERY EXECUTE Statement USING "value", "offset", "limit";
    ELSE
        RETURN QUERY EXECUTE Statement USING "value";
    END IF;
END;
$$;