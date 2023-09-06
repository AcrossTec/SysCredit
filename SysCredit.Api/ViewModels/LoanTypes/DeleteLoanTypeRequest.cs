namespace SysCredit.Api.ViewModels.LoanTypes;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.LoanTypes;

[Validator<DeleteLoanTypeValidator>]
public class DeleteLoanTypeRequest : IViewModel
{
    public long? LoanTypeId { get; set; }
}
