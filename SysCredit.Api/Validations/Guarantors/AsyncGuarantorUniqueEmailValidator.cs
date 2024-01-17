﻿namespace SysCredit.Api.Validations.Guarantors;

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
public class AsyncGuarantorUniqueEmailValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Context"></param>
    /// <param name="Email"></param>
    /// <param name="Cancellation"></param>
    /// <returns></returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Email, CancellationToken Cancellation)
    {
        var Guarantor = await Context.RootContextData[nameof(GuarantorStore)].AsStore<Guarantor>().FetchGuarantorByEmailAsync(Email);
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
    public override string Name => "AsyncGuarantorUniqueEmailValidator";
}
