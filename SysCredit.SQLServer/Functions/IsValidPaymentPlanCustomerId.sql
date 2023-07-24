CREATE FUNCTION [dbo].[IsValidPaymentPlanCustomerId]
(
    @LoanId     INT,
    @CustomerId INT
)
RETURNS INT
AS
BEGIN
    DECLARE @Result INT
    SELECT @Result = COUNT(*) FROM [dbo].[Loan] WHERE [dbo].[Loan].[LoanId] = @LoanId AND [dbo].[Loan].[CustomerId] = @CustomerId
    RETURN @Result
END
GO