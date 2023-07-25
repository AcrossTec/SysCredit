CREATE PROCEDURE [dbo].[FetchReferences]
  @Offset INT,
  @Limit INT,
  @OrderBy NVARCHAR(100),
  @SearchBy NVARCHAR(MAX)
AS
BEGIN
  DECLARE @DynamicSql NVARCHAR(MAX);

  SET @DynamicSql = N'
  SELECT * FROM Relationship
    WHERE '+ @SearchBy + '
	ORDER BY ' + @OrderBy + '
	OFFSET ' + CAST(@Offset AS NVARCHAR(10)) + ' ROWS
	FETCH NEXT ' + CAST(@Limit AS NVARCHAR(10)) + ' ROWS ONLY
  ';
  EXEC(@DynamicSQL);

END