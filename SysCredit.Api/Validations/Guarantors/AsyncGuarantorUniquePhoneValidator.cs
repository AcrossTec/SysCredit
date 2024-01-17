namespace SysCredit.Api.Validations.Guarantors;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class AsyncGuarantorUniquePhoneValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Context"></param>
    /// <param name="Phone"></param>
    /// <param name="Cancellation"></param>
    /// <returns></returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Phone, CancellationToken Cancellation)
    {
        var Guarantor = await Context.RootContextData[nameof(GuarantorStore)].AsStore<Guarantor>().FetchGuarantorByPhoneAsync(Phone);
        return Guarantor is null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ErrorCode"></param>
    /// <returns></returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return ErrorCodeMessages.GetMessageFromCode(ErrorCode)!;
    }

    /// <summary>
    /// 
    /// </summary>
    public override string Name => "AsyncGuarantorUniquePhoneValidator";
}
