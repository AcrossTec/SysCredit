DROP PROCEDURE IF EXISTS InsertGuarantor;
DELIMITER //

CREATE PROCEDURE `InsertGuarantor`(
    OUT p_GuarantorId      BIGINT,
    IN  p_Identification   VARCHAR(16),
    IN  p_Name             VARCHAR(64),
    IN  p_LastName         VARCHAR(64),
    IN  p_Gender           BIT,
    IN  p_Email            VARCHAR(64),
    IN  p_Address          VARCHAR(256),
    IN  p_Neighborhood     VARCHAR(32),
    IN  p_BussinessType    VARCHAR(32),
    IN  p_BussinessAddress VARCHAR(256),
    IN  p_Phone            VARCHAR(16)
)
BEGIN
    INSERT INTO `Guarantor` (
        `Identification`, `Name`, `LastName`, `Gender`, `Email`,
        `Address`, `Neighborhood`, `BussinessType`, `BussinessAddress`, `Phone`
    )
    VALUES (
        p_Identification, p_Name, p_LastName, p_Gender, p_Email,
        p_Address, p_Neighborhood, p_BussinessType, p_BussinessAddress, p_Phone
    );

    SET p_GuarantorId = LAST_INSERT_ID();
END //

DELIMITER ;