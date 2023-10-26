-- Create a function to insert a guarantor
CREATE OR REPLACE FUNCTION public."InsertGuarantor"(
    "Identification"   VARCHAR(16),
    "Name"             VARCHAR(64),
    "LastName"         VARCHAR(64),
    "Gender"           BIT,
    "Email"            VARCHAR(64),
    "Address"          VARCHAR(256),
    "Neighborhood"     VARCHAR(32),
    "BussinessType"    VARCHAR(32),
    "BussinessAddress" VARCHAR(256),
    "Phone"            VARCHAR(16)
) RETURNS BIGINT AS $$
DECLARE
    GuarantorId BIGINT;
BEGIN
    -- Insert guarantor
    INSERT INTO public."Guarantor" (
        "Identification", "Name",         "LastName",      "Gender",           "Email",
        "Address",        "Neighborhood", "BussinessType", "BussinessAddress", "Phone"
    ) VALUES (
        "Identification", "Name",         "LastName",      "Gender",           "Email",
        "Address",        "Neighborhood", "BussinessType", "BussinessAddress", "Phone"
    ) RETURNING "GuarantorId" INTO GuarantorId;
    
    -- Return the generated GuarantorId
    RETURN GuarantorId;
END;
