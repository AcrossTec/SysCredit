namespace SysCredit.Models;

using System.Text.Json.Serialization;

[JsonSerializable(typeof(Gender))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(Customer))]
[JsonSerializable(typeof(Guarantor))]
[JsonSerializable(typeof(Reference))]
[JsonSerializable(typeof(LoanType))]
[JsonSerializable(typeof(Relationship))]
[JsonSerializable(typeof(PaymentFrequency))]
[JsonSerializable(typeof(User[]))]
[JsonSerializable(typeof(Customer[]))]
[JsonSerializable(typeof(Guarantor[]))]
[JsonSerializable(typeof(Reference[]))]
[JsonSerializable(typeof(LoanType[]))]
[JsonSerializable(typeof(Relationship[]))]
[JsonSerializable(typeof(PaymentFrequency[]))]
public partial class ModelSerializerContext : JsonSerializerContext
{
}
