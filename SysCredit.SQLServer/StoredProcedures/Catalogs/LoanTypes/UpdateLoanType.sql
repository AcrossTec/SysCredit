﻿CREATE PROCEDURE [dbo].[UpdateLoanType]
	@LoanTypeId BIGINT,
	@Name       NVARCHAR(32)
AS
BEGIN
	UPDATE [dbo].[LoanType]
	SET
		[Name]         = @Name,
		[IsEdit]       = 1,
		[ModifiedDate] = CURRENT_TIMESTAMP
	WHERE
		[LoanTypeId] = @LoanTypeId;
END
GO
