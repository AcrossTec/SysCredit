CREATE TYPE [dbo].[ClaimRequestType]
AS TABLE
(
    [ClaimType]  NVARCHAR(30) NOT NULL,
    [ClaimValue] NVARCHAR(30) NOT NULL
);
GO