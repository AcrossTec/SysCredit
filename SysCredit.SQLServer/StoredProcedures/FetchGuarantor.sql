CREATE PROCEDURE [dbo].[FetchGuarantor] 
AS
BEGIN
    SELECT * FROM Guarantor WHERE (IsDelete = 0)
END
