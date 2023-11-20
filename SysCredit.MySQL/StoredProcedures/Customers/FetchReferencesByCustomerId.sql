DROP PROCEDURE IF EXISTS FetchReferencesByCustomerId;
DELIMITER //

CREATE PROCEDURE `FetchReferencesByCustomerId`(
    IN p_CustomerId BIGINT
)
BEGIN
    SELECT R.*
    FROM `Reference` AS R
    INNER JOIN `CustomerReference` AS CR ON R.`ReferenceId` = CR.`ReferenceId`
    WHERE R.`IsDelete` = 0 AND CR.`CustomerId` = p_CustomerId;
END //

DELIMITER ;