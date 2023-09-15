namespace SysCredit.Api.Requests.LoanType;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.LoanTypes;

[Validator<CreateLoanTypeValidator>]
public class CreateLoanTypeRequest : IRequest
{
    public string Name { get; set; } = String.Empty;
}
