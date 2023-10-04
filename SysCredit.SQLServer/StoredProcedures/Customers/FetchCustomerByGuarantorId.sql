CREATE PROCEDURE [dbo].[FetchCustomerByGuarantorId]
   @GuarantorId BIGINT
AS
BEGIN
	SELECT * FROM [CustomerGuarantor]
	WHERE  [IsDelete] = 0 AND [GuarantorId] = @GuarantorId
END
GO
