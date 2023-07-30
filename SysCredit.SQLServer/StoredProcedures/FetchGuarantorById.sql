﻿CREATE PROCEDURE [dbo].[FetchGuarantorById] @GuarantorId BIGINT
AS BEGIN
    SELECT
        G.*,
        R.[Name] AS [RelationshipName]
    FROM [dbo].[Guarantor]          AS G
    INNER JOIN [dbo].[Relationship] AS R ON  G.[RelationshipId] = R.[RelationshipId]
    WHERE G.[IsDelete] = 0 AND G.[GuarantorId] = @GuarantorId
END
GO
