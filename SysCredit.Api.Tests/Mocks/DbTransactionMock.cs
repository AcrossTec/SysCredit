namespace SysCredit.Api.Tests.Mocks;

using System.Data;
using System.Data.Common;

internal class DbTransactionMock : DbTransaction
{
    public override void Commit()
    {
    }

    public override void Rollback()
    {
    }

    protected override DbConnection? DbConnection { get; } = new DbConnectionMock();

    public override IsolationLevel IsolationLevel { get; }
}
