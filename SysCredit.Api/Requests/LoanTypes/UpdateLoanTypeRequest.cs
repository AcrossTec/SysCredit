namespace SysCredit.Api.Requests.LoanTypes;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.LoanTypes;

[Validator<UpdateLoanTypeValidator>]
public class UpdateLoanTypeRequest : IRequest
{
    public long? LoanTypeId { get; set; }

    public string Name { get; set; } = String.Empty;
}
