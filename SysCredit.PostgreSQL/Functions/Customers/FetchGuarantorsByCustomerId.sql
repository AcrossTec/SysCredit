CREATE OR REPLACE FUNCTION public."FetchGuarantorsByCustomerId" 
(
    customer_id BIGINT
)
RETURNS SETOF "Guarantor"
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT G.*
    FROM "public"."Guarantor" AS G
    INNER JOIN "public"."CustomerGuarantor" AS CG ON G."GuarantorId" = CG."GuarantorId"
    WHERE G."IsDelete" = FALSE
    AND CG."CustomerId" = customer_id;
END;
$$;