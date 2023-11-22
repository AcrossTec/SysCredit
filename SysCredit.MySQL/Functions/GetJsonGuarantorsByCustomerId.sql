DROP FUNCTION IF EXISTS GetJsonGuarantorsByCustomerId;
DELIMITER //

CREATE FUNCTION GetJsonGuarantorsByCustomerId(p_CustomerId BIGINT) RETURNS JSON DETERMINISTIC READS SQL DATA
BEGIN
    DECLARE json_result JSON;

    SELECT JSON_ARRAYAGG(
        JSON_OBJECT(
            'GuarantorId', G.`GuarantorId`,
            'Identification', G.`Identification`,
            'Name', G.`Name`,
            'LastName', G.`LastName`,
            'Gender', G.`Gender`,
            'Email', G.`Email`,
            'Address', G.`Address`,
            'Neighborhood', G.`Neighborhood`,
            'BussinessType', G.`BussinessType`,
            'BussinessAddress', G.`BussinessAddress`,
            'Phone', G.`Phone`,
            'RelationshipId', RS.`RelationshipId`,
            'RelationshipName', RS.`Name`
        )
    )
    INTO json_result
    FROM Guarantor AS G
    INNER JOIN CustomerGuarantor AS CG ON G.GuarantorId = CG.GuarantorId
    INNER JOIN Relationship AS RS ON CG.RelationshipId = RS.RelationshipId
    WHERE CG.CustomerId = p_CustomerId;

    RETURN json_result;
END //

DELIMITER ;