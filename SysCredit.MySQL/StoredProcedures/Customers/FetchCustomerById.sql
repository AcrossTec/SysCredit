DROP PROCEDURE IF EXISTS FetchCustomerById;
DELIMITER //

CREATE PROCEDURE `FetchCustomerById`(
    IN p_CustomerId BIGINT
)
BEGIN
    SELECT 
        C.*,
        R.`ReferenceId`      AS `ReferenceId`,
        R.`Identification`   AS `ReferenceIdentification`,
        R.`Name`             AS `ReferenceName`,
        R.`LastName`         AS `ReferenceLastName`,
        R.`Gender`           AS `ReferenceGender`,
        R.`Phone`            AS `ReferencePhone`,
        R.`Email`            AS `ReferenceEmail`,
        R.`Address`          AS `ReferenceAddress`,
        G.`GuarantorId`      AS `GuarantorId`,
        G.`Identification`   AS `GuarantorIdentification`,
        G.`Name`             AS `GuarantorName`,
        G.`LastName`         AS `GuarantorLastName`,
        G.`Gender`           AS `GuarantorGender`,
        G.`Email`            AS `GuarantorEmail`,
        G.`Address`          AS `GuarantorAddress`,
        G.`Neighborhood`     AS `GuarantorNeighborhood`,
        G.`BussinessType`    AS `GuarantorBussinessType`,
        G.`BussinessAddress` AS `GuarantorBussinessAddress`,
        G.`Phone`            AS `GuarantorPhone`,
        RS.`RelationshipId`  AS `GuarantorRelationshipId`,
        Rs.`Name`            AS `GuarantorRelationshipName`
    FROM `Customer` AS C
    INNER JOIN `CustomerReference` AS CR ON CR.`CustomerId`     =  C.`CustomerId`
    INNER JOIN `Reference`         AS R  ON  R.`ReferenceId`    = CR.`ReferenceId`
    INNER JOIN `CustomerGuarantor` AS CG ON CG.`CustomerId`     =  C.`CustomerId`
    INNER JOIN `Guarantor`         AS G  ON  G.`GuarantorId`    = CG.`GuarantorId`
    INNER JOIN `Relationship`      AS RS ON CG.`RelationshipId` = RS.`RelationshipId`
    WHERE C.`IsDelete` = 0 AND C.`CustomerId` = p_CustomerId;
END //

DELIMITER ;