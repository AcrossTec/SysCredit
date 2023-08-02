CREATE PROCEDURE [dbo].[FetchCustomerById] @CustomerId BIGINT
AS BEGIN
    SELECT 
        C.*,
        R.[ReferenceId]      AS [ReferenceId],
        R.[Identification]   AS [ReferenceIdentification],
        R.[Name]             AS [ReferenceName],
        R.[LastName]         AS [ReferenceLastName],
        R.[Gender]           AS [ReferenceGender],
        R.[Phone]            AS [ReferencePhone],
        R.[Email]            AS [ReferenceEmail],
        R.[Address]          AS [ReferenceAddress],
        G.[GuarantorId]      AS [GuarantorId],
        G.[Identification]   AS [GuarantorIdentification],
        G.[Name]             AS [GuarantorName],
        G.[LastName]         AS [GuarantorLastName],
        G.[Gender]           AS [GuarantorGender],
        G.[Email]            AS [GuarantorEmail],
        G.[Address]          AS [GuarantorAddress],
        G.[Neighborhood]     AS [GuarantorNeighborhood],
        G.[BussinessType]    AS [GuarantorBussinessType],
        G.[BussinessAddress] AS [GuarantorBussinessAddress],
        G.[Phone]            AS [GuarantorPhone],
        RS.[RelationshipId]  AS [GuarantorRelationshipId],
        Rs.[Name]            AS [GuarantorRelationshipName]
    FROM [dbo].[Customer] AS C
    INNER JOIN [dbo].[CustomerReference] AS CR ON CR.[CustomerId]     =  C.[CustomerId]
    INNER JOIN [dbo].[Reference]         AS R  ON  R.[ReferenceId]    = CR.[ReferenceId]
    INNER JOIN [dbo].[CustomerGuarantor] AS CG ON CG.[CustomerId]     =  C.[CustomerId]
    INNER JOIN [dbo].[Guarantor]         AS G  ON  G.[GuarantorId]    = CG.[GuarantorId]
    INNER JOIN [dbo].[Relationship]      AS RS ON CG.[RelationshipId] = RS.[RelationshipId]
    WHERE C.[IsDelete] = 0 AND C.[CustomerId] = @CustomerId
END
GO
