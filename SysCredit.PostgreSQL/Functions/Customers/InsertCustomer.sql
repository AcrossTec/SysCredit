CREATE OR REPLACE FUNCTION "public"."InsertCustomer"
(
    Identification   VARCHAR(16),
    Name             VARCHAR(64),
    LastName         VARCHAR(64),
    Gender           BIT,
    Email            VARCHAR(64),
    Address          VARCHAR(256),
    Neighborhood     VARCHAR(32),
    BussinessType    VARCHAR(32),
    BussinessAddress VARCHAR(256),
    Phone            VARCHAR(16),
    "References"     "public"."ReferenceType"[],
    "Guarantors"     "public"."CustomerGuarantorType"[]
)
RETURNS BIGINT AS $$
DECLARE
    CustomerTable  "public"."Customer";
    ReferenceTable "public"."Reference";
    CustomerId      BIGINT;
BEGIN
    -- Insertar Cliente
    INSERT INTO "public"."Customer"
    (
        "Identification", "Name",         "LastName",      "Gender",           "Email",
        "Address",        "Neighborhood", "BussinessType", "BussinessAddress", "Phone"
    )
    VALUES
    (
        Identification, Name,         LastName,      Gender,           Email,
        Address,        Neighborhood, BussinessType, BussinessAddress, Phone
    )
    RETURNING "CustomerId" INTO CustomerId;

    -- Insertar Referencias
    INSERT INTO "public"."Reference"
    (
        "Identification", "Name", "LastName", "Gender", "Phone", "Email", "Address"
    )
    SELECT * FROM UNNEST("References")
    RETURNING "ReferenceId" INTO ReferenceTable;

    -- Insertar ReferenciaCliente
    INSERT INTO "public"."CustomerReference"("CustomerId", "ReferenceId")
    SELECT TableCustomerId, "ReferenceId" FROM ReferenceTable;

    -- Insertar GarantesCliente
    INSERT INTO "public"."CustomerGuarantor"("CustomerId", "GuarantorId", "RelationshipId")
    SELECT TableCustomerId, GuarantorId, "RelationshipId" FROM UNNEST("Guarantors");

    -- Confirmar la transacción
    COMMIT;

    -- Asignar el valor de "CustomerId"
    RETURN CustomerId;
END;
$$ LANGUAGE plpgsql;