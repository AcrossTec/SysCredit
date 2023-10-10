namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Interfaces.Services;

using SysCredit.Models;

using static Constants.ErrorCodePrefix;

/// <summary>
/// 
/// </summary>
[Service<IReferenceService>]
[ServiceModel<Reference>]
[ErrorCategory(nameof(ReferenceService))]
[ErrorCodePrefix(ReferenceServicePrefix)]
#pragma warning disable CS9113 // Parameter is unread.
public partial class ReferenceService(ILogger<ReferenceService> Logger)
{
}

#pragma warning restore CS9113 // Parameter is unread.
