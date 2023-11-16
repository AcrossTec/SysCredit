INSERT INTO "PaymentFrequency" AS "TargetTable"
VALUES (1,'Semanal'    ),
       (2,'Quincenal'  ),
       (3,'Mensual'    ),
       (4,'Anual'      ),
       (5,'Trimestral' )
ON CONFLICT ("PaymentFrequencyId") DO UPDATE
SET "Name" = EXCLUDED."Name";