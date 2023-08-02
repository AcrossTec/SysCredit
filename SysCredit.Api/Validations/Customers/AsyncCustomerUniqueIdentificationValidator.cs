﻿namespace SysCredit.Api.Validations.Customers;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Models;
using SysCredit.Api.Stores;

using System.Threading;
using System.Threading.Tasks;

public class AsyncCustomerUniqueIdentificationValidator<T> : AsyncPropertyValidator<T, string?>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Identification, CancellationToken Cancellation)
    {
        var Guarantor = await Context.RootContextData[nameof(GuarantorStore)].AsStore<Guarantor>().FetchGuarantorByIdentification(Identification);
        return Guarantor is null;
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Ya existe un registro con este valor.";
    }

    public override string Name => "AsyncUniquePhoneValidator";
}
