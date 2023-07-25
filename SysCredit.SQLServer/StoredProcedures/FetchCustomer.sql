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
AS
BEGIN
    SELECT * FROM Customer WHERE (IsDelete = 0)
END
