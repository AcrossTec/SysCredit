CREATE PROCEDURE [dbo].[FetchGuarantorByPhone] @Phone NVARCHAR(16)
AS BEGIN
    SELECT G.*
    FROM [dbo].[Guarantor] AS G
    WHERE G.[IsDelete] = 0 AND G.[Phone] = @Phone
END
GO
