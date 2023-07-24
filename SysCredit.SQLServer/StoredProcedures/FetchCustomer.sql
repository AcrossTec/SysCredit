CREATE PROCEDURE [dbo].[FetchCustomer] 
AS
BEGIN
    SELECT * FROM Customer WHERE (DeletedDate IS NULL AND IsDelete = 0)
END
