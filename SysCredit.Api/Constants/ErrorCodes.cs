﻿namespace SysCredit.Api.Constants;

using static ErrorCodePrefix;
using static ErrorCodeNumber;

public static class ErrorCodes
{
    public const string DATAC0001 = $"{CustomerStorePrefix}{_0001}";
    public const string DATAC0002 = $"{CustomerStorePrefix}{_0002}";
    public const string DATAC0003 = $"{CustomerStorePrefix}{_0003}";
    public const string DATAC0004 = $"{CustomerStorePrefix}{_0004}";
    public const string DATAC0005 = $"{CustomerStorePrefix}{_0005}";
    public const string DATAC0006 = $"{CustomerStorePrefix}{_0006}";

    public const string DATAG0001 = $"{GuarantorStorePrefix}{_0001}";
    public const string DATAG0002 = $"{GuarantorStorePrefix}{_0002}";
    public const string DATAG0003 = $"{GuarantorStorePrefix}{_0003}";
    public const string DATAG0004 = $"{GuarantorStorePrefix}{_0004}";
    public const string DATAG0005 = $"{GuarantorStorePrefix}{_0005}";
    public const string DATAG0006 = $"{GuarantorStorePrefix}{_0006}";

    public const string DATALT0001 = $"{LoanTypeStorePrefix}{_0001}";
    public const string DATALT0002 = $"{LoanTypeStorePrefix}{_0002}";
    public const string DATALT0003 = $"{LoanTypeStorePrefix}{_0003}";
    public const string DATALT0004 = $"{LoanTypeStorePrefix}{_0004}";
    public const string DATALT0005 = $"{LoanTypeStorePrefix}{_0005}";
    public const string DATALT0006 = $"{LoanTypeStorePrefix}{_0006}";
}
