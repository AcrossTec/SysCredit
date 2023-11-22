DROP FUNCTION IF EXISTS GetJsonGuarantorsByCustomerId;
DELIMITER //

CREATE FUNCTION GetJsonReferencesByCustomerId(p_CustomerId BIGINT) RETURNS JSON DETERMINISTIC READS SQL DATA
BEGIN
    DECLARE json_result JSON;

    SELECT JSON_ARRAYAGG(
        JSON_OBJECT(
            'ReferenceId', R.`ReferenceId`,
            'Identification', R.`Identification`,
            'Name', R.`Name`,
            'LastName', R.`LastName`,
            'Gender', R.`Gender`,
            'Phone', R.`Phone`,
            'Email', R.`Email`,
            'Address', R.`Address`
        )
    ) INTO json_result
    FROM Reference AS R
    INNER JOIN `CustomerReference` AS CR ON R.`ReferenceId` = CR.`ReferenceId`
    WHERE CR.`CustomerId` = p_CustomerId;

    RETURN json_result;
END //

DELIMITER ;