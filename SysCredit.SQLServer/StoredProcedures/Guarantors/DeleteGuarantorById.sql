CREATE PROCEDURE [dbo].[DeleteGuarantorById]
	@GuarantorId BIGINT
AS
BEGIN
	UPDATE [dbo].[Guarantor]
	SET
		[IsDelete]		= 1,
		[IsEdit]		= 1,
		[ModifiedDate]	= CURRENT_TIMESTAMP,
		[DeletedDate]	= CURRENT_TIMESTAMP
	WHERE
		[GuarantorId] = @GuarantorId
END
GO