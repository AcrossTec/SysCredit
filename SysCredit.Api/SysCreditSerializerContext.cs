namespace SysCredit.Api;

using System.Text.Json.Serialization;

using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Customers;
using SysCredit.Api.Requests.Guarantors;
using SysCredit.Api.Requests.LoanTypes;
using SysCredit.Api.Requests.PaymentFrequencies;
using SysCredit.Api.Requests.References;
using SysCredit.Api.Requests.Relationships;

[JsonSerializable(typeof(PaginationRequest))]
[JsonSerializable(typeof(SearchRequest))]
[JsonSerializable(typeof(CreateCustomerRequest))]
[JsonSerializable(typeof(CustomerGuarantorRequest))]
[JsonSerializable(typeof(CustomerIdRequest))]
[JsonSerializable(typeof(GuarantorAndCustomerIdsRequest))]
[JsonSerializable(typeof(CreateGuarantorRequest))]
[JsonSerializable(typeof(DeleteGuarantorRequest))]
[JsonSerializable(typeof(GuarantorIdRequest))]
[JsonSerializable(typeof(CreateLoanTypeRequest))]
[JsonSerializable(typeof(DeleteLoanTypeRequest))]
[JsonSerializable(typeof(UpdateLoanTypeRequest))]
[JsonSerializable(typeof(CreatePaymentFrequencyRequest))]
[JsonSerializable(typeof(DeletePaymentFrequencyRequest))]
[JsonSerializable(typeof(UpdatePaymentFrequencyRequest))]
[JsonSerializable(typeof(CreateReferenceRequest))]
[JsonSerializable(typeof(CreateRelationshipRequest))]
[JsonSerializable(typeof(DeleteRelationshipRequest))]
[JsonSerializable(typeof(UpdateRelationshipRequest))]
[JsonSerializable(typeof(CustomerGuarantorRequest[]))]
[JsonSerializable(typeof(CreateReferenceRequest[]))]
public partial class SysCreditSerializerContext : JsonSerializerContext
{
}
