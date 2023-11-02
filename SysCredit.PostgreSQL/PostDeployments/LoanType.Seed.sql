/*
Plantilla de Script posterior a la implementaci�n para la tabla "LoanType".							
--------------------------------------------------------------------------------------
Este Script contiene instrucciones SQL que insertar� datos en la tabla "LoanType" o
una actualizaci�n en caso de conflicto debido a un duplicado en la columna "LoanTypeId". 
Utilice la sintaxis de PostgreSQL para ejecutar este script de inserci�n.
Ejemplo:      \i /ruta/al/archivo/LoanType.Seed.sql
--------------------------------------------------------------------------------------
*/

/*
Cl�usula ON CONFLICT 
https://www.postgresql.org/docs/current/sql-insert.html#SQL-ON-CONFLICT
*/

INSERT INTO "LoanType" AS "TargetTable"
VALUES 
	(1, 'Nuevo1'           ),
	(2, 'Renovaci�n'       ),
	(3, 'Paralelo'		   ),
    (4, 'Reestructuraci�n' ),
	(5, 'Pago �nico'       )
ON CONFLICT ("LoanTypeId") 
DO UPDATE
	SET "Name" = EXCLUDED."Name";