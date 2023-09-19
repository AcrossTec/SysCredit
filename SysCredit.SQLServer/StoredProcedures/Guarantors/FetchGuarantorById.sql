CREATE PROCEDURE [dbo].[FetchGuarantorById] @GuarantorId BIGINT
AS BEGIN
    SELECT G.*
    FROM [dbo].[Guarantor] AS G
    WHERE G.[IsDelete] = 0 AND G.[GuarantorId] = @GuarantorId
END
GO
