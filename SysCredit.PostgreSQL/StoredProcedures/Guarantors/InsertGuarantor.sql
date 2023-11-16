CREATE OR REPLACE PROCEDURE "public"."InsertGuarantor"
(
    INOUT guarantor_id      BIGINT,
    IN    identification    VARCHAR(64),
    IN    name              VARCHAR(64),
    IN    last_name         VARCHAR(64),
    IN    gender		    BIT,
    IN    email             VARCHAR(64),
    IN    address           VARCHAR(64),
    IN    neighborhood      VARCHAR(32),
    IN    bussiness_type    VARCHAR(32),
    IN    bussiness_address VARCHAR(256),
    IN    phone             VARCHAR(16)
)
AS $$
BEGIN

    INSERT INTO "public"."Guarantor"
    (
        "Identification", "Name",         "LastName",      "Gender",           "Email",
        "Address",        "Neighborhood", "BussinessType", "BussinessAddress", "Phone" 
    )
    VALUES
    (
        identification, Name,         last_name,      gender,            email,
        address,        neighborhood, bussiness_type, bussiness_address, phone
    )
    RETURNING "GuarantorId" INTO guarantor_id;

END; 
$$ LANGUAGE plpgsql;