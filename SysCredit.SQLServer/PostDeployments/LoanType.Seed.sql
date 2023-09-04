/*
Plantilla de script posterior a la implementación							
--------------------------------------------------------------------------------------
 Este archivo contiene instrucciones de SQL que se anexarán al script de compilación.		
 Use la sintaxis de SQLCMD para incluir un archivo en el script posterior a la implementación.			
 Ejemplo:      :r .\miArchivo.sql								
 Use la sintaxis de SQLCMD para hacer referencia a una variable en el script posterior a la implementación.		
 Ejemplo:      :setvar TableName miTabla							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

MERGE INTO [LoanType] AS [TargetTable]
USING ( VALUES ( 1, N'Nuevo'            ),
               ( 2, N'Renovación'       ),
               ( 3, N'Paralelo'         ),
               ( 4, N'Reestructuración' ),
               ( 5, N'Pago Único'       )
      ) AS [SourceTable] ( [LoanTypeId], [Name] )
ON [TargetTable].[LoanTypeId] = [SourceTable].[LoanTypeId]
WHEN MATCHED THEN
   UPDATE SET [Name] = [SourceTable].[Name]
WHEN NOT MATCHED THEN
   INSERT ( [Name] ) VALUES ( [SourceTable].[Name] );