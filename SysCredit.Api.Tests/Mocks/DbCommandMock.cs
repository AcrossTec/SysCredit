namespace SysCredit.Api.Tests.Mocks;

using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

internal class DbCommandMock : DbCommand
{
    public override void Cancel()
    {
    }

    protected override DbParameter CreateDbParameter()
    {
        return new DbParameterMock();
    }

    protected override DbDataReader ExecuteDbDataReader(CommandBehavior Behavior)
    {
        return new DbDataReaderMock();
    }

    public override int ExecuteNonQuery()
    {
        return default!;
    }

    public override object? ExecuteScalar()
    {
        return default!;
    }

    public override void Prepare()
    {
    }

    [AllowNull]
    public override string CommandText { get; set; }

    public override int CommandTimeout { get; set; }

    public override CommandType CommandType { get; set; }

    protected override DbConnection? DbConnection { get; set; } = new DbConnectionMock();

    [AllowNull]
    protected override DbParameterCollection DbParameterCollection { get; } = new DbParameterCollectionMock();

    protected override DbTransaction? DbTransaction { get; set; } = new DbTransactionMock();

    public override bool DesignTimeVisible { get; set; }

    public override UpdateRowSource UpdatedRowSource { get; set; }
}
