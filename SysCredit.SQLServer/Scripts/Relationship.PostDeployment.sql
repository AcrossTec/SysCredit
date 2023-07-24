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

MERGE INTO Relationship TargetTable
  USING ( VALUES (1, 'Papa'),
                 (2, 'Mama'),
                 (3, 'Hermana'),
                 (4, 'Hermano'),
                 (5, 'Tío'),
                 (6, 'Abuelo')
  ) AS SourceTable ( [RelationshipId], [Name] )
      ON TargetTable.RelationshipId = SourceTable.[RelationshipId]
WHEN MATCHED THEN 
   UPDATE
      SET Name = SourceTable.[Name]
WHEN NOT MATCHED THEN
   INSERT ( [Name] ) VALUES ( SourceTable.[Name] );
