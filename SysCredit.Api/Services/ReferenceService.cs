namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Interfaces;

using static Constants.ErrorCodePrefix;

/// <summary>
/// 
/// </summary>
[Service<IReferenceService>]
[ErrorCategory(nameof(ReferenceService))]
[ErrorCodePrefix(ReferenceServicePrefix)]
#pragma warning disable CS9113 // Parameter is unread.
public class ReferenceService(ILogger<ReferenceService> Logger) : IReferenceService;
#pragma warning restore CS9113 // Parameter is unread.
