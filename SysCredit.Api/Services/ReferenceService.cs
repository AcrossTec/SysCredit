namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Interfaces;

/// <summary>
/// 
/// </summary>
[Service<IReferenceService>]
[ErrorCategory(ErrorCategories.ReferenceService)]
public class ReferenceService : IReferenceService
{
}
