/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/

/*
MERGE (Transact-SQL)
https://learn.microsoft.com/en-us/sql/t-sql/statements/merge-transact-sql?view=sql-server-ver16

SQL Server MERGE
https://www.sqlservertutorial.net/sql-server-basics/sql-server-merge/
https://www.sqlservertutorial.org/sql-server-merge/

MERGE target_table USING source_table
ON merge_condition
WHEN MATCHED
    THEN update_statement
WHEN NOT MATCHED BY TARGET
    THEN insert_statement
WHEN NOT MATCHED BY SOURCE
    THEN DELETE;

Table Value Constructor (Transact-SQL)
https://learn.microsoft.com/en-us/sql/t-sql/queries/table-value-constructor-transact-sql?view=sql-server-ver16

OUTPUT clause (Transact-SQL)
https://learn.microsoft.com/en-us/sql/t-sql/queries/output-clause-transact-sql?view=sql-server-ver16

SQL Server MERGE Statement overview and examples
https://www.sqlshack.com/sql-server-merge-statement-overview-and-examples/

Understanding the SQL MERGE statement
https://www.sqlshack.com/understanding-the-sql-merge-statement/

MERGE Statement in SQL Explained
https://www.geeksforgeeks.org/merge-statement-sql-explained/
*/

MERGE INTO [Relationship] AS [TargetTable]
USING ( VALUES (  1, N'Papá'     ),
               (  2, N'Mamá'     ),
               (  3, N'Hermano'  ),
               (  4, N'Hermana'  ),
               (  5, N'Tío'      ),
               (  6, N'Tía'      ),
               (  7, N'Sobrino'  ),
               (  8, N'Sobrina'  ),
               (  9, N'Nieto'    ),
               ( 10, N'Nieta'    ),
               ( 11, N'Esposo'   ),
               ( 12, N'Esposa'   ),
               ( 13, N'Hijo'     ),
               ( 14, N'Hija'     ),
               ( 15, N'Primo'    ),
               ( 16, N'Prima'    ),
               ( 17, N'Abuelo'   ),
               ( 18, N'Abuela'   ),
               ( 19, N'Amigo'    ),
               ( 20, N'Amiga'    ),
               ( 21, N'Cuñado'   ),
               ( 22, N'Cuñada'   ),
               ( 23, N'Padrasto' ),
               ( 24, N'Madrasta' ),
               ( 25, N'Novio'    ),
               ( 26, N'Novia'    )
      ) AS [SourceTable] ( [RelationshipId], [Name] )
ON [TargetTable].[RelationshipId] = [SourceTable].[RelationshipId]
WHEN MATCHED THEN
   UPDATE SET [Name] = [SourceTable].[Name]
WHEN NOT MATCHED THEN
   INSERT ( [Name] ) VALUES ( [SourceTable].[Name] );
