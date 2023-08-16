﻿namespace SysCredit.Api.Validations.Guarantors;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

public class AsyncGuarantorUniqueEmailValidator<T> : AsyncPropertyValidator<T, string?>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Email, CancellationToken Cancellation)
    {
        var Guarantor = await Context.RootContextData[nameof(GuarantorStore)].AsStore<Guarantor>().FetchGuarantorByEmail(Email);
        return Guarantor is null;
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Ya existe un registro con este valor.";
    }

    public override string Name => "AsyncGuarantorUniqueEmailValidator";
}
