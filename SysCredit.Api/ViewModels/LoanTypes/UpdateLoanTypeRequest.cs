namespace SysCredit.Api.ViewModels.LoanTypes;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.LoanTypes;

[Validator<UpdateLoanTypeValidator>]
public class UpdateLoanTypeRequest : IViewModel
{
    public long? LoanTypeId { get; set; }

    public string Name { get; set; } = String.Empty;
}
