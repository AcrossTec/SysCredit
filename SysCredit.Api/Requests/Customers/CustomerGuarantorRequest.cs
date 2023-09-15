namespace SysCredit.Api.Requests.Customers;

public class CustomerGuarantorRequest : IRequest
{
    public long? LoanId { get; set; }

    public long GuarantorId { get; set; }

    public long RelationshipId { get; set; }
}
