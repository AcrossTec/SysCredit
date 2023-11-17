CREATE OR REPLACE FUNCTION public."GetJsonReferencesByCustomerId" (
    customer_id BIGINT
)
RETURNS JSON
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN (
        SELECT json_agg(json_build_object(
            'ReferenceId',    R."ReferenceId",
            'Identification', R."Identification",
            'Name',           R."Name",
            'LastName',       R."LastName",
            'Gender',         R."Gender",
            'Phone',          R."Phone",
            'Email',          R."Email",
            'Address',        R."Address"
        ))::JSON
        FROM "public"."Reference" AS R
        INNER JOIN "public"."CustomerReference" AS CR ON R."ReferenceId" = CR."ReferenceId"
        WHERE CR."CustomerId" = customer_id
    );
END;
$$;