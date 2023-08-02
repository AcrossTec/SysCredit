namespace SysCredit.Api.ViewModels.Customers;

public class CustomerGuarantorRequest : IViewModel
{
    public long? LoanId { get; set; }

    public long GuarantorId { get; set; }

    public long RelationshipId { get; set; }
}
