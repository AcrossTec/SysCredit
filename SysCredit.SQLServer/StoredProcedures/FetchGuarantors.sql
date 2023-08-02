CREATE PROCEDURE [dbo].[FetchGuarantors]
AS BEGIN
    SELECT G.*
    FROM [dbo].[Guarantor] AS G
    WHERE G.[IsDelete] = 0
    ORDER BY G.[Name] ASC, G.[LastName] ASC
END
GO
