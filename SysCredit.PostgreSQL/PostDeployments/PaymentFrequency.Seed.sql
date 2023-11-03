-- NUMBER: 3.2
/*
Plantilla de script posterior a la implementación para la tabla "PaymentFrequency".						
--------------------------------------------------------------------------------------
Este script contiene instrucciones de sintaxis PostgreSQL para insertar o actualizar datos en 
la tabla PaymentFrequency" y en caso de conflicto debido a un duplicado en la columna "PaymentFrequencyId"
se hace uso de la clausula ON CONFLICT. 
--------------------------------------------------------------------------------------
*/

INSERT INTO "PaymentFrequency" AS "TargetTable"
VALUES (1,'Semanal'    ),
       (2,'Quincenal'  ),
       (3,'Mensual'    ),
       (4,'Anual'      ),
       (5,'Trimestral' )
ON CONFLICT ("PaymentFrequencyId") DO UPDATE
SET "Name" = EXCLUDED."Name";