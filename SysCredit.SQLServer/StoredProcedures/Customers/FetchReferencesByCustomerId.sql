CREATE PROCEDURE [dbo].[FetchReferencesByCustomerId] @CustomerId BIGINT
AS BEGIN
    SELECT R.*
    FROM [dbo].[Reference] AS R
    INNER JOIN [dbo].[CustomerReference] AS CR ON R.[ReferenceId] = CR.[ReferenceId]
    WHERE R.[IsDelete] = 0 AND CR.[CustomerId] = @CustomerId
END
GO
