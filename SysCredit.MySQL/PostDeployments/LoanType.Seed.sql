INSERT INTO `LoanType` (`LoanTypeId`, `Name`) 
VALUES (1, 'Nuevo'),
       (2, 'Renovaci�n'),
       (3, 'Paralelo'),
       (4, 'Reestructuraci�n'),
       (5, 'Pago �nico')
ON DUPLICATE KEY UPDATE `Name` = VALUES(`Name`);