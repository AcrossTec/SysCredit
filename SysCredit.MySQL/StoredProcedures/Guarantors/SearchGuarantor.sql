DROP PROCEDURE IF EXISTS SearchGuarantor;
DELIMITER //

CREATE PROCEDURE `SearchGuarantor`(
    IN  p_Value  TEXT,
    IN  p_Offset INT,
    IN  p_Limit  INT
)
BEGIN
    SET @Statement = 'SELECT * FROM `Guarantor`
         WHERE `IsDelete` = 0
         AND (`Identification` LIKE CONCAT(\'%\', IFNULL(?, `Identification`), \'%\')
         OR   `Name`           LIKE CONCAT(\'%\', IFNULL(?, `Name`          ), \'%\')
         OR   `LastName`       LIKE CONCAT(\'%\', IFNULL(?, `LastName`      ), \'%\')
         OR   `Phone`          LIKE CONCAT(\'%\', IFNULL(?, `Phone`         ), \'%\')
         OR   `Email`          LIKE CONCAT(\'%\', IFNULL(?, `Email`         ), \'%\'))
         ORDER BY `Name` ASC, `LastName` ASC';
    
    SET @p_Value = p_Value;
    SET @p_Offset = p_Offset;
    SET @p_Limit = p_Limit;
    
    IF (p_Offset IS NOT NULL) AND (p_Limit IS NOT NULL) THEN
        SET @Statement = CONCAT(@Statement, ' LIMIT ?, ?');
    END IF;

    -- Execute the dynamic SQL statement
    PREPARE stmt FROM @Statement;
    EXECUTE stmt USING @p_Value, @p_Value, @p_Value, @p_Value, @p_Value, @p_Offset, @p_Limit;
    DEALLOCATE PREPARE stmt;
END //

DELIMITER ;