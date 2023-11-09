CREATE OR REPLACE FUNCTION "public"."InsertGuarantor"
(
    Identification   VARCHAR(64),
    Name             VARCHAR(64),
    LastName         VARCHAR(64),
    Gender		     BIT,
    Email            VARCHAR(64),
    Address          VARCHAR(64),
    Neighborhood     VARCHAR(32),
    BussinessType    VARCHAR(32),
    BussinessAddress VARCHAR(256),
    Phone            VARCHAR(16)
)
RETURNS BIGINT
AS $$
DECLARE
    GuarantorId BIGINT;
BEGIN

    INSERT INTO "public"."Guarantor"
    (
        "Identification", "Name",         "LastName",      "Gender",           "Email",
        "Address",        "Neighnorhood", "BussinessType", "BussinessAddress", "Phone" 
    )
    VALUES
    (
        Identification, Name,         LastName,      Gender,           Email,
        Address,        Neighnorhood, BussinessType, BussinessAddress, Phone
    )
    RETURNING "GuarantorId" INTO GuarantorId;
	
    RETURN GuarantorId;
END; 
$$ LANGUAGE plpgsql;