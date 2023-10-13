CREATE PROCEDURE [dbo].[InsertLoanType]
    @Name       NVARCHAR(32),
    @LoanTypeId BIGINT OUTPUT
AS
BEGIN
    INSERT INTO [dbo].[LoanType] 
    (
        [Name]
	)
    VALUES 
    (
        @Name
    )

    -- Retrieve the generated LoanTypeId and store it in the @LoanTypeId output parameter
    SET @LoanTypeId = SCOPE_IDENTITY()
END
