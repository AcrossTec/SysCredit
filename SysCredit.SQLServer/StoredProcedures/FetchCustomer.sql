/*
SQL Server OFFSET FETCH
https://www.sqlservertutorial.net/sql-server-basics/sql-server-alias/

SQL Server SELECT TOP
https://www.sqlservertutorial.net/sql-server-basics/sql-server-select-top/

SQL Server SELECT DISTINCT
https://www.sqlservertutorial.net/sql-server-basics/sql-server-select-distinct/

SQL Server LIKE
https://www.sqlservertutorial.net/sql-server-basics/sql-server-like/
*/

CREATE PROCEDURE [dbo].[FetchCustomer] 
  @Offset INT,
  @Limit INT,
  @OrderBy NVARCHAR(100),
  @SearchBy NVARCHAR(MAX)
AS
BEGIN
  DECLARE @DynamicSql NVARCHAR(MAX);

  SET @DynamicSql = N'
  SELECT * FROM Customer
    WHERE '+ @SearchBy + '
	ORDER BY ' + @OrderBy + '
	OFFSET ' + CAST(@Offset AS NVARCHAR(10)) + ' ROWS
	FETCH NEXT ' + CAST(@Limit AS NVARCHAR(10)) + ' ROWS ONLY
  ';
  EXEC(@DynamicSQL);

END