CREATE PROCEDURE [dbo].[FetchReferencesByCustomerId] @CustomerId BIGINT
AS BEGIN
    SELECT R.*
    FROM [dbo].[Reference] AS R
    INNER JOIN [dbo].[CustomerReference] AS CR ON R.[ReferenceId] = CR.[ReferenceId]
    WHERE CR.[CustomerId] = @CustomerId
END
GO
