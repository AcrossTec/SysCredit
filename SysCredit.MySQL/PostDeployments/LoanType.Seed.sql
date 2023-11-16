INSERT INTO `LoanType` (`LoanTypeId`, `Name`) 
VALUES (1, 'Nuevo'),
       (2, 'Renovación'),
       (3, 'Paralelo'),
       (4, 'Reestructuración'),
       (5, 'Pago Único')
ON DUPLICATE KEY UPDATE `Name` = VALUES(`Name`);