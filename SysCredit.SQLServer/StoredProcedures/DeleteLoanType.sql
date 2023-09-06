CREATE PROCEDURE [dbo].[DeleteLoanType]
    @LoanTypeId BIGINT
AS
BEGIN
    -- Perform the logical delete
    UPDATE [dbo].[LoanType]
    SET
        [IsDelete]     = 1,                  -- Set IsDelete to 1 to indicate a logical delete
        [IsEdit]       = 1,                  -- Set IsEdit to 1 to indicate a logical delete
        [ModifiedDate] = CURRENT_TIMESTAMP,  -- Update ModifiedDate to the current timestamp
        [DeletedDate]  = CURRENT_TIMESTAMP   -- Set DeletedDate to the current timestamp
    WHERE
        [LoanTypeId] = @LoanTypeId;
END
GO
