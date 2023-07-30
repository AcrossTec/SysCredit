CREATE PROCEDURE [dbo].[FetchGuarantors] @Offset INT, @Limit INT
AS BEGIN
    SELECT
        G.*,
        R.[Name] AS [RelationshipName]
    FROM [dbo].[Guarantor]          AS G
    INNER JOIN [dbo].[Relationship] AS R ON  G.[RelationshipId] = R.[RelationshipId]
    WHERE G.[IsDelete] = 0
    ORDER BY G.[Name] ASC, G.[LastName] ASC
    OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY;
END
GO
