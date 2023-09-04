﻿namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.LoanType;

public class CreateLoanTypeValidator : AbstractValidator<CreateLoanTypeRequest>
{
    public CreateLoanTypeValidator()
    {
        RuleFor(T => T.Name)
            .NotEmpty()
            .NotNull()
            .LoanTypeUniqueNameAsync()
            .WithName("Nombre");
    }
}
