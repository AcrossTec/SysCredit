INSERT INTO `PaymentFrequency` (`PaymentFrequencyId`, `Name`) 
VALUES (1, 'Semanal'),
       (2, 'Quincenal'),
       (3, 'Mensual'),
       (4, 'Anual'),
       (5, 'Trimestral')
ON DUPLICATE KEY UPDATE `Name` = VALUES(`Name`);