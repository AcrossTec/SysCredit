CREATE TYPE [dbo].[CustomerGuarantorType]
AS TABLE
(
    [LoanId]         BIGINT NULL,
    [GuarantorId]    BIGINT NOT NULL,
    [RelationshipId] BIGINT NOT NULL
);
GO
