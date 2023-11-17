CREATE OR REPLACE FUNCTION public."FetchReferencesByCustomerId" (
    customer_id BIGINT
)
RETURNS SETOF "Reference"
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT R.*
    FROM "public"."Reference" AS R
    INNER JOIN "public"."CustomerReference" AS CR ON R."ReferenceId" = CR."ReferenceId"
    WHERE R."IsDelete" = FALSE AND CR."CustomerId" = customer_id;
END;
$$;