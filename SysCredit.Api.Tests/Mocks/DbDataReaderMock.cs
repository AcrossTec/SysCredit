namespace SysCredit.Api.Tests.Mocks;

using System;
using System.Collections;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

internal class DbDataReaderMock : DbDataReader
{
    public override bool GetBoolean(int Ordinal)
    {
        return default!;
    }

    public override byte GetByte(int Ordinal)
    {
        return default!;
    }

    public override long GetBytes(int Ordinal, long DataOffset, byte[]? Buffer, int BufferOffset, int Length)
    {
        return default!;
    }

    public override char GetChar(int Ordinal)
    {
        return default!;
    }

    public override long GetChars(int Ordinal, long DataOffset, char[]? Buffer, int BufferOffset, int Length)
    {
        return default!;
    }

    public override string GetDataTypeName(int Ordinal)
    {
        return default!;
    }

    public override DateTime GetDateTime(int Ordinal)
    {
        return default!;
    }

    public override decimal GetDecimal(int Ordinal)
    {
        return default!;
    }

    public override double GetDouble(int Ordinal)
    {
        return default!;
    }

    public override IEnumerator GetEnumerator()
    {
        return default!;
    }

    [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)]
    public override Type GetFieldType(int Ordinal)
    {
        return default!;
    }

    public override float GetFloat(int Ordinal)
    {
        return default!;
    }

    public override Guid GetGuid(int Ordinal)
    {
        return default!;
    }

    public override short GetInt16(int Ordinal)
    {
        return default!;
    }

    public override int GetInt32(int Ordinal)
    {
        return default!;
    }

    public override long GetInt64(int Ordinal)
    {
        return default!;
    }

    public override string GetName(int Ordinal)
    {
        return default!;
    }

    public override int GetOrdinal(string name)
    {
        return default!;
    }

    public override string GetString(int Ordinal)
    {
        return default!;
    }

    public override object GetValue(int Ordinal)
    {
        return default!;
    }

    public override int GetValues(object[] values)
    {
        return default!;
    }

    public override bool IsDBNull(int Ordinal)
    {
        return default!;
    }

    public override bool NextResult()
    {
        return default!;
    }

    public override bool Read()
    {
        return default!;
    }

    public override int Depth { get; }

    public override int FieldCount { get; }

    public override bool HasRows { get; }

    public override bool IsClosed { get; }

    public override object this[int Ordinal] => default!;

    public override object this[string name] => default!;

    public override int RecordsAffected { get; }
}
