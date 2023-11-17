CREATE OR REPLACE PROCEDURE "public"."InsertCustomer"(
    INOUT customer_id       BIGINT,
    IN    identification    VARCHAR(16),
    IN    name              VARCHAR(64),
    IN    last_name         VARCHAR(64),
    IN    gender            BIT,
    IN    email             VARCHAR(64),
    IN    address           VARCHAR(256),
    IN    neighborhood      VARCHAR(32),
    IN    bussiness_type    VARCHAR(32),
    IN    bussiness_address VARCHAR(256),
    IN    phone             VARCHAR(16),
    IN    "references"      "public"."ReferenceType"[],
    IN    guarantors        "public"."CustomerGuarantorType"[]
)
LANGUAGE plpgsql
AS $$
DECLARE
    table_reference_id BIGINT;
    table_customer_id  BIGINT;
BEGIN
    -- Insert Customer
    INSERT INTO "public"."Customer"
    (
        "Identification", "Name",         "LastName",      "Gender",           "Email",
        "Address",        "Neighborhood", "BussinessType", "BussinessAddress", "Phone"
    )
    VALUES
    (
        identification, name,         last_name,      gender,            email,
        address,        neighborhood, bussiness_type, bussiness_address, phone
    )
    RETURNING "CustomerId" INTO table_customer_id;

    -- Insert References
    INSERT INTO "public"."Reference"
    (
        "Identification", "Name", "LastName", "Gender", "Phone", "Email", "Address"
    )
    SELECT * FROM UNNEST("references")
    RETURNING "ReferenceId" INTO table_reference_id;

    -- Insert CustomerReference
    INSERT INTO "public"."CustomerReference"("CustomerId", "ReferenceId")
    SELECT table_customer_id, table_reference_id;

    -- Insert CustomerGuarantor
    INSERT INTO "public"."CustomerGuarantor"("CustomerId", "GuarantorId", "RelationshipId")
    SELECT table_customer_id, "GuarantorId", "RelationshipId" FROM UNNEST(guarantors);

    customer_id = table_customer_id;
END;
$$;