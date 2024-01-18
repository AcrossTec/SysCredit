namespace SysCredit.Api.Managers;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Interfaces.Managers;

using SysCredit.Models;

using static Constants.ErrorCodePrefix;

/// <summary>
/// 
/// </summary>
[ManagerModel<Reference>]
[ManagerInterface<IReferenceManager>]
[ErrorCategory(nameof(ReferenceManager))]
[ErrorCodePrefix(ReferenceManagerPrefix)]
#pragma warning disable CS9113 // Parameter is unread.
public partial class ReferenceManager(ILogger<ReferenceManager> Logger);

#pragma warning restore CS9113 // Parameter is unread.
