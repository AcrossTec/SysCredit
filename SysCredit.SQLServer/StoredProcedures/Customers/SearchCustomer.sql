/*
    sp_executesql (Transact-SQL)
    https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-executesql-transact-sql?view=sql-server-ver16

    The Curse and Blessings of Dynamic SQL
    https://www.sommarskog.se/dynamic_sql.html

    IF...ELSE (Transact-SQL)
    https://learn.microsoft.com/en-us/sql/t-sql/language-elements/if-else-transact-sql?view=sql-server-ver16

    SQL Injection
    https://learn.microsoft.com/en-us/sql/relational-databases/security/sql-injection?view=sql-server-ver16
*/

CREATE PROCEDURE [dbo].[SearchCustomer] @Value NVARCHAR(MAX), @Offset INT = NULL, @Limit INT = NULL
AS BEGIN
    DECLARE @Statement NVARCHAR(MAX) = N'
        SELECT
            C.*,
            [dbo].[GetJsonReferencesByCustomerId](C.[CustomerId]) AS [JsonReferences],
            [dbo].[GetJsonGuarantorsByCustomerId](C.[CustomerId]) AS [JsonGuarantors]
        FROM [dbo].[Customer] AS C
        WHERE C.[IsDelete] = 0
          AND C.[Identification] LIKE CONCAT(''%'', ISNULL(@Value, C.[Identification]), ''%'')
           OR C.[Name]           LIKE CONCAT(''%'', ISNULL(@Value, C.[Name]          ), ''%'')
           OR C.[LastName]       LIKE CONCAT(''%'', ISNULL(@Value, C.[LastName]      ), ''%'')
           OR C.[Phone]          LIKE CONCAT(''%'', ISNULL(@Value, C.[Phone]         ), ''%'')
           OR C.[Email]          LIKE CONCAT(''%'', ISNULL(@Value, C.[Email]         ), ''%'')
        ORDER BY C.[Name] ASC, C.[LastName] ASC'

    DECLARE @Parameters NVARCHAR(MAX) = N'@Value NVARCHAR(MAX)'

    IF (@Offset IS NOT NULL) AND (@Limit IS NOT NULL)
    BEGIN
        SELECT @Parameters = CONCAT(@Parameters,  N', @Offset INT, @Limit INT')
        SELECT @Statement = CONCAT(@Statement, N' OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY')
        EXEC sp_executesql @Statement, @Parameters, @Value, @Offset, @Limit
    END
    ELSE
    BEGIN
        EXEC sp_executesql @Statement, @Parameters, @Value
    END
END
GO
