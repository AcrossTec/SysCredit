CREATE OR REPLACE FUNCTION "FetchGuarantorByCustomerIdAndGuarantorId"
(
    customer_id BIGINT,
    guarantor_id BIGINT
)
RETURNS SETOF "GuarantorRelationshipInfo"
AS $$
BEGIN
    RETURN QUERY 
    SELECT 
        G.*,
        R."RelationshipId" AS "RelationshipId",
        R."Name"           AS "RelationshipName"
    FROM 
        "Guarantor" AS G
    INNER JOIN 
        "CustomerGuarantor" AS CG ON G."GuarantorId" = CG."GuarantorId"
    INNER JOIN 
        "Relationship" AS R ON CG."RelationshipId" = R."RelationshipId"
    WHERE 
        G."IsDelete"    = FALSE 
        AND CG."CustomerId" = customer_id
        AND G."GuarantorId" = guarantor_id
    LIMIT 1;
END;
$$ LANGUAGE plpgsql;