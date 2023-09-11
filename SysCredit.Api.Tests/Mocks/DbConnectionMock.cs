namespace SysCredit.Api.Tests.Mocks;

using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

internal class DbConnectionMock : DbConnection
{
    protected override DbTransaction BeginDbTransaction(IsolationLevel IsolationLevel)
    {
        return new DbTransactionMock();
    }

    public override void ChangeDatabase(string DatabaseName)
    {
    }

    public override void Close()
    {
    }

    protected override DbCommand CreateDbCommand()
    {
        return new DbCommandMock();
    }

    public override void Open()
    {
    }

    [AllowNull]
    public override string ConnectionString { get; set; }

    public override string Database => default!;

    public override string DataSource => default!;

    public override string ServerVersion => default!;

    public override ConnectionState State { get; }
}
