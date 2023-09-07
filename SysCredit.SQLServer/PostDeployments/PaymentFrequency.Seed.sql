MERGE INTO [PaymentFrequency] AS [TargetTable]
USING ( VALUES ( 1, N'Semanal'   ),
               ( 2, N'Quincenal' ),
               ( 3, N'Mensual'   ),
               ( 4, N'Anual'     ),
               ( 5, N'Trimestral')
      ) AS [SourceTable] ( [PaymentFrequencyId], [Name] )
ON [TargetTable].[PaymentFrequencyId] = [SourceTable].[PaymentFrequencyId]
WHEN MATCHED THEN
   UPDATE SET [Name] = [SourceTable].[Name]
WHEN NOT MATCHED THEN
   INSERT ( [Name] ) VALUES ( [SourceTable].[Name] );