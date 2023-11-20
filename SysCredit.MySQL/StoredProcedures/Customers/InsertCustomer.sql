DROP PROCEDURE IF EXISTS InsertCustomer;
DELIMITER //

CREATE PROCEDURE `InsertCustomer`(
    OUT p_CustomerId       BIGINT,
    IN  p_Identification   VARCHAR(16),
    IN  p_Name             VARCHAR(64),
    IN  p_LastName         VARCHAR(64),
    IN  p_Gender           INT,
    IN  p_Email            VARCHAR(64),
    IN  p_Address          VARCHAR(256),
    IN  p_Neighborhood     VARCHAR(32),
    IN  p_BussinessType    VARCHAR(32),
    IN  p_BussinessAddress VARCHAR(256),
    IN  p_Phone            VARCHAR(16),
    IN  p_References       JSON,
    IN  p_Guarantors       JSON
)
BEGIN
    DECLARE v_TableCustomerId BIGINT;
    DECLARE v_ReferenceId BIGINT;
    
    SET @References_Identification = JSON_EXTRACT(p_References, '$.Identification');
    SET @References_Name = JSON_EXTRACT(p_References, '$.Name');
    SET @References_LastName = JSON_EXTRACT(p_References, '$.LastName');
    SET @References_Gender = JSON_EXTRACT(p_References, '$.Gender');
    SET @References_Phone = JSON_EXTRACT(p_References, '$.Phone');
    SET @References_Email = JSON_EXTRACT(p_References, '$.Email');
    SET @References_Address = JSON_EXTRACT(p_References, '$.Address');
    
    SET @v_Gender = CAST(JSON_UNQUOTE(@References_Gender) AS UNSIGNED);
    
    SET @Guarantors_GuarantorId = JSON_EXTRACT(p_Guarantors, '$.GuarantorId');
    SET @Guarantors_RelationshipId = JSON_EXTRACT(p_Guarantors, '$.RelationshipId');

    -- Insert Customer
    INSERT INTO `Customer` (
        `Identification`, `Name`, `LastName`, `Gender`, `Email`,
        `Address`, `Neighborhood`, `BussinessType`, `BussinessAddress`, `Phone`
    )
    VALUES (
        p_Identification, p_Name, p_LastName, p_Gender, p_Email,
        p_Address, p_Neighborhood, p_BussinessType, p_BussinessAddress, p_Phone
    );

    -- Get CustomerId
    SET v_TableCustomerId = LAST_INSERT_ID();

    -- Insert References
    INSERT INTO `Reference` (`Identification`, `Name`, `LastName`, `Gender`, `Phone`, `Email`, `Address`)
    VALUES
        (JSON_UNQUOTE(@References_Identification), JSON_UNQUOTE(@References_Name), JSON_UNQUOTE(@References_LastName), @v_Gender, 
         JSON_UNQUOTE(@References_Phone), JSON_UNQUOTE(@References_Email), JSON_UNQUOTE(@References_Address));

    -- Get ReferenceId
    SET v_ReferenceId = LAST_INSERT_ID();

    -- Insert CustomerReference
    INSERT INTO `CustomerReference` (`CustomerId`, `ReferenceId`)
    VALUES (v_TableCustomerId, v_ReferenceId);

    -- Insert CustomerGuarantor
    INSERT INTO `CustomerGuarantor` (`CustomerId`, `GuarantorId`, `RelationshipId`)
    VALUES 
        (v_TableCustomerId, JSON_UNQUOTE(@Guarantors_GuarantorId), JSON_UNQUOTE(@Guarantors_RelationshipId));

    -- Set output parameter
    SET p_CustomerId = v_TableCustomerId;
END //

DELIMITER ;