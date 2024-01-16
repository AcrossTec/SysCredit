namespace SysCredit.DataTransferObject;

using System.Text.Json.Serialization;

using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

[JsonSerializable(typeof(CustomerInfo))]
[JsonSerializable(typeof(ReferenceInfo))]
[JsonSerializable(typeof(GuarantorInfo))]
[JsonSerializable(typeof(LoanInfo))]
[JsonSerializable(typeof(LoanTypeInfo))]
[JsonSerializable(typeof(RelationshipInfo))]
[JsonSerializable(typeof(PaymentFrequencyInfo))]
[JsonSerializable(typeof(CustomerGuarantorInfo))]
[JsonSerializable(typeof(FetchCustomer))]
[JsonSerializable(typeof(FetchGuarantor))]
[JsonSerializable(typeof(SearchCustomer))]
[JsonSerializable(typeof(CustomerInfo[]))]
[JsonSerializable(typeof(ReferenceInfo[]))]
[JsonSerializable(typeof(GuarantorInfo[]))]
[JsonSerializable(typeof(LoanInfo[]))]
[JsonSerializable(typeof(LoanTypeInfo[]))]
[JsonSerializable(typeof(RelationshipInfo[]))]
[JsonSerializable(typeof(PaymentFrequencyInfo[]))]
[JsonSerializable(typeof(CustomerGuarantorInfo[]))]
[JsonSerializable(typeof(FetchCustomer[]))]
[JsonSerializable(typeof(FetchGuarantor[]))]
[JsonSerializable(typeof(SearchCustomer[]))]
public partial class DataTransferObjectSerializerContext : JsonSerializerContext
{
}
