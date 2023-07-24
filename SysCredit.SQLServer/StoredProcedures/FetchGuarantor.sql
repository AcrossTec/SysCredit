CREATE PROCEDURE [dbo].[FetchGuarantor] 
AS
BEGIN
    SELECT * FROM Guarantor WHERE (DeletedDate IS NULL AND IsDelete = 0)
END
