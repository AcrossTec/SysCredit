CREATE PROCEDURE [dbo].[UpdatePaymentFrequency]
    @PaymentFrequencyId BIGINT,
    @Name				NVARCHAR(32)
AS
BEGIN
    UPDATE [dbo].[PaymentFrequency]
    SET
        [Name]		   = @Name,
        [IsEdit]	   = 1,
        [ModifiedDate] = CURRENT_TIMESTAMP
    WHERE
        [PaymentFrequencyId] = @PaymentFrequencyId;
END
GO
