INSERT INTO "LoanType" AS "TargetTable"
VALUES (1, 'Nuevo'           ),
       (2, 'Renovaci�n'      ),
       (3, 'Paralelo'        ),
       (4, 'Reestructuraci�n'),
       (5, 'Pago �nico'      )
ON CONFLICT ("LoanTypeId") DO UPDATE
SET "Name" = EXCLUDED."Name";