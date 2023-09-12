namespace SysCredit.Api.Stores;

using SysCredit.Api.Attributes;

using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(nameof(ReferenceStore))]
[ErrorCodePrefix(ReferenceStorePrefix)]
public static class ReferenceStore;
