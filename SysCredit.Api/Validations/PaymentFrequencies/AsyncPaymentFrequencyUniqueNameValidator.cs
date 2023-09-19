﻿namespace SysCredit.Api.Validations.PaymentFrequencies;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

public class AsyncPaymentFrequencyUniqueNameValidator<T> : AsyncPropertyValidator<T, string?>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Name, CancellationToken Cancellation)
    {
        var PaymentFrequency = await Context.RootContextData[nameof(PaymentFrequencyStore)].AsStore<PaymentFrequency>().FetchPaymentFrequencyByName(Name);
        return PaymentFrequency is null;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "'{PropertyName}'. Ya existe un registro con este nombre";
    }

    public override string Name => "AsyncPaymentFrequencyUniqueNameValidator";

}