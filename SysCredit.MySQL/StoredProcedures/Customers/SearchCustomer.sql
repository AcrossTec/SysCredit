DROP PROCEDURE IF EXISTS SearchCustomer;
DELIMITER //

CREATE PROCEDURE `SearchCustomer`(
    IN  p_Value  TEXT,
    IN  p_Offset INT,
    IN  p_Limit  INT
)
BEGIN
    SET @Statement = 'SELECT 
                         C.*, 
                         GetJsonReferencesByCustomerId(C.CustomerId) AS JsonReferences, 
                         GetJsonGuarantorsByCustomerId(C.CustomerId) AS JsonGuarantors 
					FROM `Customer` AS C 
                    WHERE C.IsDelete = 0 
                      AND (C.Identification LIKE CONCAT(\'%\', IFNULL(?, C.Identification), \'%\') 
                       OR C.Name            LIKE CONCAT(\'%\', IFNULL(?, C.Name), \'%\') 
                       OR C.LastName        LIKE CONCAT(\'%\', IFNULL(?, C.LastName), \'%\') 
                       OR C.Phone           LIKE CONCAT(\'%\', IFNULL(?, C.Phone), \'%\') 
                       OR C.Email           LIKE CONCAT(\'%\', IFNULL(?, C.Email), \'%\')) 
                    ORDER BY C.Name ASC, C.LastName ASC ';
    
    SET @p_Value = p_Value;
    SET @p_Offset = p_Offset;
    SET @p_Limit = p_Limit;

    IF (p_Offset IS NOT NULL) AND (p_Limit IS NOT NULL) THEN
        SET @Statement = CONCAT(@Statement, 'LIMIT ?, ?');
    END IF;

    -- Execute the dynamic SQL statement
    PREPARE stmt FROM @Statement;
    EXECUTE stmt USING @p_Value, @p_Value, @p_Value, @p_Value, @p_Value, @p_Offset, @p_Limit;
    DEALLOCATE PREPARE stmt;
END //

DELIMITER ;