CREATE PROCEDURE [dbo].[InsertGuarantor]
    @Identification   NVARCHAR(16),
    @Name             NVARCHAR(64),
    @LastName         NVARCHAR(64),
    @Address          NVARCHAR(256),
    @Neighborhood     NVARCHAR(32),
    @BussinessType    NVARCHAR(32),
    @BussinessAddress NVARCHAR(256),
    @Phone            NVARCHAR(16),
    @RelationshipId   BIGINT
AS
BEGIN
  INSERT INTO Guarantor(Identification, Name, LastName, 
                        Address, Neighborhood, BussinessType, BussinessAddress, 
                        Phone, CreatedDate, ModifiedDate, 
                        DeletedDate, IsEdit, IsDelete, RelationshipId)

  VALUES (@Identification, @Name, @LastName, @Address,
          @Neighborhood, @BussinessType,
          @BussinessAddress, @Phone, GETUTCDATE(), 
          NULL, NULL, 0, 0, @RelationshipId)

  SELECT SCOPE_IDENTITY();
END
