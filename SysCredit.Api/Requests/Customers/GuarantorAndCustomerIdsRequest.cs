namespace SysCredit.Api.Requests.Customers;

public record class GuarantorAndCustomerIdsRequest : IRequest
{
    public long? CustomerId { get; set; }
    public long? GuarantorId { get; set; }
}
