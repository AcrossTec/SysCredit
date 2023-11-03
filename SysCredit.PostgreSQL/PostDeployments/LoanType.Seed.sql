-- NUMBER: 3.1
/*
Plantilla de script posterior a la implementación para la tabla "LoanType".								
--------------------------------------------------------------------------------------
Este script contiene instrucciones de sintaxis PostgreSQL para insertar o actualizar datos en 
la tabla "LoanType" y en caso de conflicto debido a un duplicado en la columna "LoanTypeId"
se hace uso de la clausula ON CONFLICT. 
https://www.postgresql.org/docs/current/sql-insert.html#SQL-ON-CONFLICT  			
--------------------------------------------------------------------------------------
*/

INSERT INTO "LoanType" AS "TargetTable"
VALUES (1, 'Nuevo'            ),
       (2, 'Renovación'       ),
       (3, 'Paralelo'         ),
       (4, 'Reestructuración' ),
       (5, 'Pago Único'       )
ON CONFLICT ("LoanTypeId") DO UPDATE
SET "Name" = EXCLUDED."Name";