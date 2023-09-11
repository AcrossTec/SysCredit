namespace SysCredit.Api.Tests.Mocks;

using System;
using System.Collections;
using System.Data.Common;

internal class DbParameterCollectionMock : DbParameterCollection
{
    public override int Add(object Value)
    {
        return default!;
    }

    public override void AddRange(Array Values)
    {
    }

    public override void Clear()
    {
    }

    public override bool Contains(object Value)
    {
        return default!;
    }

    public override bool Contains(string Value)
    {
        return default!;
    }

    public override void CopyTo(Array Array, int Index)
    {
    }

    public override IEnumerator GetEnumerator()
    {
        return default!;
    }

    protected override DbParameter GetParameter(int Index)
    {
        return default!;
    }

    protected override DbParameter GetParameter(string ParameterName)
    {
        return default!;
    }

    public override int IndexOf(object Value)
    {
        return default!;
    }

    public override int IndexOf(string ParameterName)
    {
        return default!;
    }

    public override void Insert(int Index, object Value)
    {
    }

    public override void Remove(object Value)
    {
    }

    public override void RemoveAt(int Index)
    {
    }

    public override void RemoveAt(string ParameterName)
    {
    }

    protected override void SetParameter(int Index, DbParameter Value)
    {
    }

    protected override void SetParameter(string ParameterName, DbParameter Value)
    {
    }

    public override int Count { get; }

    public override object SyncRoot { get; } = new object();
}
