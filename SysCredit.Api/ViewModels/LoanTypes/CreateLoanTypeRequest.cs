namespace SysCredit.Api.ViewModels.LoanType;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.LoanTypes;

[Validator<CreateLoanTypeValidator>]
public class CreateLoanTypeRequest : IViewModel
{
    public string Name { get; set; } = String.Empty;
}
