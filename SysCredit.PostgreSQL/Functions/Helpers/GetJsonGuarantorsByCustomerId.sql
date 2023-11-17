CREATE OR REPLACE FUNCTION public."GetJsonGuarantorsByCustomerId"
(
    customer_id BIGINT
)
RETURNS JSON
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN (
        SELECT json_agg(json_build_object(
            'GuarantorId',      G."GuarantorId",
            'Identification',   G."Identification",
            'Name',             G."Name",
            'LastName',         G."LastName",
            'Gender',           G."Gender",
            'Email',            G."Email",
            'Address',          G."Address",
            'Neighborhood',     G."Neighborhood",
            'BussinessType',    G."BussinessType",
            'BussinessAddress', G."BussinessAddress",
            'Phone',            G."Phone",
            'RelationshipId',   RS."RelationshipId",
            'RelationshipName', RS."Name"
        ))::JSON
        FROM "public"."Guarantor" AS G
        INNER JOIN "public"."CustomerGuarantor" AS CG ON G."GuarantorId"     = CG."GuarantorId"
        INNER JOIN "public"."Relationship"      AS RS ON CG."RelationshipId" = RS."RelationshipId"
        WHERE CG."CustomerId" = customer_id
    );
END;
$$;