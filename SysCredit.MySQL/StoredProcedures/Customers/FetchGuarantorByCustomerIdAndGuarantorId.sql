DROP PROCEDURE IF EXISTS FetchGuarantorByCustomerIdAndGuarantorId;
DELIMITER //

CREATE PROCEDURE `FetchGuarantorByCustomerIdAndGuarantorId`(
    IN p_CustomerId BIGINT,
    IN p_GuarantorId BIGINT
)
BEGIN
    SELECT 
        G.*,
        R.`RelationshipId` AS `RelationshipId`,
        R.`Name`           AS `RelationshipName`
    FROM `Guarantor` AS G
    INNER JOIN `CustomerGuarantor` AS CG ON G.`GuarantorId` = CG.`GuarantorId`
    INNER JOIN `Relationship` AS R ON CG.`RelationshipId` = R.`RelationshipId`
    WHERE G.`IsDelete` = 0 
      AND CG.`CustomerId` = p_CustomerId
      AND G.`GuarantorId` = p_GuarantorId
    LIMIT 1;
END //

DELIMITER ;