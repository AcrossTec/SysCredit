CREATE PROCEDURE [dbo].[FetchGuarantorByIdentification] @Identification NVARCHAR(16)
AS BEGIN
    SELECT G.*
    FROM [dbo].[Guarantor] AS G
    WHERE G.[IsDelete] = 0 AND G.[Identification] = @Identification
END
GO
