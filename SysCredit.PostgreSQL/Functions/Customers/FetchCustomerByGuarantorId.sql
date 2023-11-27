CREATE OR REPLACE FUNCTION "public"."FetchCustomerByGuarantorId"
(
    guarantor_id BIGINT
)
RETURNS SETOF "Customer"
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT C.*
    FROM "Customer" AS C
    INNER JOIN "CustomerGuarantor" AS CG 
    ON C."CustomerId" = CG."CustomerId"
    WHERE NOT C."IsDelete"
    AND CG."GuarantorId" = guarantor_id;
END;
$$;