CREATE PROCEDURE [dbo].[FetchGuarantorByEmail] @Email NVARCHAR(64)
AS BEGIN
    SELECT G.*
    FROM [dbo].[Guarantor] AS G
    WHERE G.[IsDelete] = 0 AND G.[Email] = @Email
END
GO
