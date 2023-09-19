CREATE PROCEDURE [dbo].[FetchGuarantorsTop] @Offset INT, @Limit INT
AS BEGIN
    SELECT G.*
    FROM [dbo].[Guarantor] AS G
    WHERE G.[IsDelete] = 0
    ORDER BY G.[Name] ASC, G.[LastName] ASC
    OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY;
END
GO
