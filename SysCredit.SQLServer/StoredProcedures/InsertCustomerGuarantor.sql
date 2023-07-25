CREATE PROCEDURE [dbo].[InsertCustomerGuarantor]
    @CustomerId BIGINT,
    @GuarantorId BIGINT,
    @LoanId BIGINT = NULL,
    @LoanDate DATETIME2 = NULL,
    @IsEdit BIT = 0,
    @IsDelete BIT = 0
AS
BEGIN
    INSERT INTO CustomerGuarantor(CustomerId, GuarantorId, LoanId, LoanDate, IsEdit, IsDelete)
    VALUES (@CustomerId, @GuarantorId, @LoanId, @LoanDate, @IsEdit, @IsDelete)
END