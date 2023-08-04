CREATE FUNCTION [dbo].[GetJsonReferencesByCustomerId] (@CustomerId BIGINT) RETURNS NVARCHAR(MAX)
AS BEGIN
   RETURN
   (
     SELECT R.*
     FROM [dbo].[Reference] AS R
     INNER JOIN [dbo].[CustomerReference] AS CR ON R.[ReferenceId] = CR.[ReferenceId]
     WHERE CR.[CustomerId] = @CustomerId
     FOR JSON AUTO, INCLUDE_NULL_VALUES
   )
END
