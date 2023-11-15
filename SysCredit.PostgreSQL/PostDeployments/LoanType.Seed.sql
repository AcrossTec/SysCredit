INSERT INTO "LoanType" AS "TargetTable"
VALUES (1, 'Nuevo'           ),
       (2, 'Renovación'      ),
       (3, 'Paralelo'        ),
       (4, 'Reestructuración'),
       (5, 'Pago único'      )
ON CONFLICT ("LoanTypeId") DO UPDATE
SET "Name" = EXCLUDED."Name";