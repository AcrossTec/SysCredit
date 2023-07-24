CREATE PROCEDURE [dbo].[InsertCustomer] 
    @Identification   NVARCHAR(16),
    @Name             NVARCHAR(64),
    @LastName         NVARCHAR(64),
    @Address          NVARCHAR(256),
    @Neighborhood     NVARCHAR(32),
    @BussinessType    NVARCHAR(32),
    @BussinessAddress NVARCHAR(256),
    @Phone            NVARCHAR(16)
AS
BEGIN
    INSERT INTO Customer(Identification, Name, LastName, Address, Neighborhood, BussinessType, BussinessAddress, Phone, CreatedDate, ModifiedDate, DeletedDate, IsEdit, IsDelete)
    VALUES (@Identification, @Name, @LastName, @Address, @Neighborhood, @BussinessType, @BussinessAddress, @Phone, GETUTCDATE(), NULL, NULL, 0, 0)

    RETURN 0
END
