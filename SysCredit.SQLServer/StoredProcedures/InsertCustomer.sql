/*
SQL Server Transaction
https://www.sqlservertutorial.net/sql-server-basics/sql-server-transaction/

SQL Server CTE
https://www.sqlservertutorial.net/sql-server-basics/sql-server-cte/

SQL Server Recursive CTE
https://www.sqlservertutorial.net/sql-server-basics/sql-server-recursive-cte/

SQL Server PIVOT
https://www.sqlservertutorial.net/sql-server-basics/sql-server-pivot/

OUTPUT clause (Transact-SQL)
https://learn.microsoft.com/en-us/sql/t-sql/queries/output-clause-transact-sql?view=sql-server-ver16

Return data from a stored procedure
https://learn.microsoft.com/en-us/sql/relational-databases/stored-procedures/return-data-from-a-stored-procedure?view=sql-server-ver16

RETURN (Transact-SQL)
https://learn.microsoft.com/en-us/sql/t-sql/language-elements/return-transact-sql?view=sql-server-ver16
*/

CREATE PROCEDURE [dbo].[InsertCustomer]
    @CustomerId       BIGINT OUTPUT,
    @Identification   NVARCHAR(16) ,
    @Name             NVARCHAR(64) ,
    @LastName         NVARCHAR(64) ,
    @Gender           BIT          ,
    @Email            NVARCHAR(64) ,
    @Address          NVARCHAR(256),
    @Neighborhood     NVARCHAR(32) ,
    @BussinessType    NVARCHAR(32) ,
    @BussinessAddress NVARCHAR(256),
    @Phone            NVARCHAR(16) ,
    @References       [dbo].[ReferenceType] READONLY,
    @Guarantors       [dbo].[GuarantorType] READONLY
AS BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    DECLARE @CustomerTable   TABLE (CustomerId BIGINT);
    DECLARE @ReferenceTable  TABLE (ReferenceId BIGINT);
    DECLARE @TableCustomerId BIGINT;

    -- Insert Custumer

    INSERT INTO [dbo].[Customer]
    (
        [Identification], [Name]        , [LastName]    ,  [Gender]          , [Email],
        [Address]       , [Neighborhood], [BussinessType], [BussinessAddress], [Phone]
    )
    OUTPUT INSERTED.[CustomerId] INTO @CustomerTable
    VALUES
    (
        @Identification, @Name        , @LastName    ,  @Gender          , @Email,
        @Address       , @Neighborhood, @BussinessType, @BussinessAddress, @Phone
    )

    -- Get CustomerId

    SELECT @TableCustomerId = CustomerId FROM @CustomerTable

    -- Insert References

    INSERT INTO [dbo].[Reference]
    (
        [Identification], [Name], [LastName], [Gender], [Phone], [Email], [Address]
    )
    OUTPUT INSERTED.[ReferenceId] INTO @ReferenceTable
    SELECT * FROM @References

    -- Insert CustomerReference

    INSERT INTO [dbo].[CustomerReference]([CustomerId], [ReferenceId])
    SELECT @TableCustomerId, [ReferenceId] FROM @ReferenceTable

    -- Insert CustomerGuarantor

    INSERT INTO [dbo].[CustomerGuarantor]([CustomerId], [GuarantorId])
    SELECT @TableCustomerId, [GuarantorId] FROM @Guarantors

    COMMIT TRANSACTION;
    SELECT @CustomerId = @TableCustomerId
END
GO
