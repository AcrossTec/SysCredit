/*

Stored Procedure Output Parameters
https://www.sqlservertutorial.net/sql-server-stored-procedures/stored-procedure-output-parameters/

*/

CREATE PROCEDURE [dbo].[InsertGuarantor]
    @GuarantorId      BIGINT OUTPUT,
    @Identification   NVARCHAR(16),
    @Name             NVARCHAR(64),
    @LastName         NVARCHAR(64),
    @Gender           BIT,
    @Email            NVARCHAR(64),
    @Address          NVARCHAR(256),
    @Neighborhood     NVARCHAR(32),
    @BussinessType    NVARCHAR(32),
    @BussinessAddress NVARCHAR(256),
    @Phone            NVARCHAR(16),
    @RelationshipId   BIGINT
AS BEGIN
  INSERT INTO [dbo].[Guarantor]
  (
    [Identification]  , [Name]   , [LastName]      , [Gender]       ,
    [Email]           , [Address], [Neighborhood]  , [BussinessType],
    [BussinessAddress], [Phone]  , [RelationshipId]
  )
  VALUES
  (
    @Identification  , @Name   , @LastName      , @Gender       ,
    @Email           , @Address, @Neighborhood  , @BussinessType,
    @BussinessAddress, @Phone  , @RelationshipId
  )

  SELECT @GuarantorId = SCOPE_IDENTITY();
END
GO
