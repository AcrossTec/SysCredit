CREATE PROCEDURE [dbo].[FetchCustomerById]
	@CustomerId BIGINT
AS
BEGIN
	SELECT * FROM Customer WHERE IsDelete = 0 AND CustomerId = @CustomerId
END