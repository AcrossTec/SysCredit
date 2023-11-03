-- NUMBER: 3.3
/*
Plantilla de script posterior a la implementaci�n para la tabla "Relationship".							
--------------------------------------------------------------------------------------
Este script contiene instrucciones de sintaxis PostgreSQL para insertar o actualizar datos en 
la tabla "Relationship" y en caso de conflicto debido a un duplicado en la columna "RelationshipId"
se hace uso de la clausula ON CONFLICT. 
--------------------------------------------------------------------------------------
*/

INSERT INTO "Relationship" ("RelationshipId", "Name")
VALUES (1, 'Pap�'      ),
       (2, 'Mam�'      ),
       (3, 'Hermano'   ),
       (4, 'Hermana'   ),
       (5, 'T�o'       ),
       (6, 'T�a'       ),
       (7, 'Sobrino'   ),
       (8, 'Sobrina'   ),
       (9, 'Nieto'     ),
       (10, 'Nieta'    ),
       (11, 'Esposo'   ),
       (12, 'Esposa'   ),
       (13, 'Hijo'     ),
       (14, 'Hija'     ),
       (15, 'Primo'    ),
       (16, 'Prima'    ),
       (17, 'Abuelo'   ),
       (18, 'Abuela'   ),
       (19, 'Amigo'    ),
       (20, 'Amiga'    ),
       (21, 'Cu�ado'   ),
       (22, 'Cu�ada'   ),
       (23, 'Padrasto' ),
       (24, 'Madrasta' ),
       (25, 'Novio'    ),
       (26, 'Novia'    )
ON CONFLICT ("RelationshipId") DO UPDATE
SET "Name" = EXCLUDED."Name";