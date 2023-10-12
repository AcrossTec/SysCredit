namespace SysCredit.Api.Requests.Guarantors;

using SysCredit.Api.Validations.Guarantors;
using SysCredit.Api.Attributes;

[Validator<DeleteGuarantorValidator>]
public class DeleteGuarantorRequest : IRequest
{
    public long? GuarantorId { get; set; }
}