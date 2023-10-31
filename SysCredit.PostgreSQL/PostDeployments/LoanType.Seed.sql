/*
Plantilla de Script posterior a la implementación para la tabla "LoanType".							
--------------------------------------------------------------------------------------
Este Script contiene instrucciones SQL que insertará datos en la tabla "LoanType" o
una actualización en caso de conflicto debido a un duplicado en la columna "LoanTypeId". 
Utilice la sintaxis de PostgreSQL para ejecutar este script de inserción.
Ejemplo:      \i /ruta/al/archivo/LoanType.Seed.sql
--------------------------------------------------------------------------------------
*/

/*
Cláusula ON CONFLICT 
https://www.postgresql.org/docs/current/sql-insert.html#SQL-ON-CONFLICT
*/

INSERT INTO "LoanType" AS "TargetTable"
VALUES 
	(1, 'Nuevo1'           ),
	(2, 'Renovación'       ),
	(3, 'Paralelo'		   ),
    (4, 'Reestructuración' ),
	(5, 'Pago Único'       )
ON CONFLICT ("LoanTypeId") 
DO UPDATE
	SET "Name" = EXCLUDED."Name";