namespace SysCredit.Api;

using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.SwaggerUI;

using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Customers;
using SysCredit.Api.Requests.Guarantors;
using SysCredit.Api.Requests.LoanTypes;
using SysCredit.Api.Requests.PaymentFrequencies;
using SysCredit.Api.Requests.References;
using SysCredit.Api.Requests.Relationships;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

/// <summary>
///     Clase usada como contexto de Serialización y Deserialización para cadenas Json.
/// </summary>
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
[JsonSerializable(typeof(ProblemDetails))]
[JsonSerializable(typeof(HttpValidationProblemDetails))]
[JsonSerializable(typeof(IResponse<IAsyncEnumerable<PaymentFrequencyInfo>>))]
[JsonSerializable(typeof(IResponse<IAsyncEnumerable<PaymentFrequency>>))]
[JsonSerializable(typeof(IResponse<ProblemHttpResult>))]
[JsonSerializable(typeof(IResponse<ValidationProblemDetails>))]
[JsonSerializable(typeof(Response<IAsyncEnumerable<PaymentFrequencyInfo>>))]
[JsonSerializable(typeof(Response<IAsyncEnumerable<PaymentFrequency>>))]
[JsonSerializable(typeof(Response<ProblemHttpResult>))]
[JsonSerializable(typeof(Response<ValidationProblemDetails>))]
[JsonSerializable(typeof(ConfigObject))]
[JsonSerializable(typeof(UrlDescriptor))]
[JsonSerializable(typeof(ModelRendering))]
[JsonSerializable(typeof(DocExpansion))]
[JsonSerializable(typeof(SubmitMethod))]
[JsonSerializable(typeof(OAuthConfigObject))]
[JsonSerializable(typeof(InterceptorFunctions))]
[JsonSourceGenerationOptions(UseStringEnumConverter = true, PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified)]
public partial class SysCreditSerializerContext : JsonSerializerContext
{
}
