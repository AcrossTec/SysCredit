namespace SysCredit.Api.Validations.Customers;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Customers;

public class CustomerGuarantorValidator : AbstractValidator<CustomerGuarantorRequest>
{
    public CustomerGuarantorValidator()
    {
        RuleFor(Cg => Cg.GuarantorId)
            .ExistsGuarantorAsync()
            .WithName("Fiador");

        RuleFor(Cg => Cg.RelationshipId)
            .ExistsRelationshipAsync()
            .WithName("Parentesco");
    }
}
