namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Customers;

using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;

/// <summary>
///     Endpoints para las distintas operaciones relacionadas al cliente.
/// </summary>
/// <param name="CustomerService">
///     Servicio que tiene toda la funcionalidad y operaciones relacionados al cliente.
/// </param>
/// <param name="Logger">
///     Objeto ILogger para el controlador.
/// </param>
[ApiController]
[Route("Api/[Controller]")]
public class CustomerController(ICustomerService CustomerService) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<CustomerInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<IAsyncEnumerable<CustomerInfo>>> FetchCustomersAsync()
    {
        return await CustomerService.FetchCustomerAsync().ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpGet("Search")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<SearchCustomer>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<IAsyncEnumerable<SearchCustomer>>> SearchCustomerAsync([FromQuery] SearchRequest Request)
    {
        return await CustomerService.SearchCustomerAsync(Request).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CustomerId"></param>
    /// <returns></returns>
    [HttpGet("{CustomerId}")]
    [ProducesResponseType(typeof(IResponse<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchCustomerByIdAsync(long? CustomerId)
    {
        return await CustomerService.FetchCustomerByIdAsync(CustomerId).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Identification"></param>
    /// <returns></returns>
    [HttpGet("ByIdentification/{Identification}")]
    [ProducesResponseType(typeof(IResponse<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchCustomerByIdentificationAsync(string? Identification)
    {
        return await CustomerService.FetchCustomerByIdentificationAsync(Identification).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Email"></param>
    /// <returns></returns>
    [HttpGet("ByEmail/{Email}")]
    [ProducesResponseType(typeof(IResponse<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchCustomerByEmailAsync(string? Email)
    {
        return await CustomerService.FetchCustomerByEmailAsync(Email).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Phone"></param>
    /// <returns></returns>
    [HttpGet("ByPhone/{Phone}")]
    [ProducesResponseType(typeof(IResponse<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchCustomerByPhoneAsync(string? Phone)
    {
        return await CustomerService.FetchCustomerByPhoneAsync(Phone).ToResponseAsync();
    }

    /// <summary>
    ///     Crear un nuevo cliente en la base de datos.
    /// </summary>
    /// <remarks>
    ///     POST: /Api/Customer
    ///     
    ///     Request: {
    ///         "Identification": String,
    ///         "Name":           String,
    ///     }
    ///     
    ///     Success Response: {
    ///         Status: {
    ///             HasError: Boolean,
    ///         },
    ///         Data: {
    ///             Id: Number
    ///         }
    ///     }
    ///     
    ///     Error Response: {
    ///         Status: {
    ///             HasError: Boolean,
    ///         },
    ///         Data: RequestInfo
    ///     }
    /// </remarks>
    /// <param name="Request">
    ///     Datos usados para crear el cliente.
    /// </param>
    /// <returns>
    ///     Regresa el nuevo Id del cliente creado.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(IResponse<ErrorResponse>))]
    public async Task<ActionResult<IResponse<EntityId>>> InsertCustomerAsync([FromBody] CreateCustomerRequest Request)
    {
        var ServiceResult = await CustomerService.InsertCustomerAsync(Request);
        return StatusCode(StatusCodes.Status201Created, await ServiceResult.ToResponseAsync());
    }

    /// <summary>
    ///     Obtiene todos las referencias de un cliente
    /// </summary>
    /// <param name="Request">Se envia el id del cliente</param>
    /// <returns>Regresa una lista de referencias del cliente</returns>
    [HttpGet("{CustomerId}/References")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<ReferenceInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<CustomerIdRequest>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<IAsyncEnumerable<ReferenceInfo>>> FetchReferencesByCustomerIdAsync([FromRoute] CustomerIdRequest Request)
    {
        return await CustomerService.FetchReferenceByCustomerIdAsync(Request).ToResponseAsync();
    }

    /// <summary>
    ///     Obtiene todos los fiadores de un cliente
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpGet("{CustomerId}/Guarantors")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<ReferenceInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<CustomerIdRequest>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<IAsyncEnumerable<GuarantorInfo>>> FetchGuarantorsByCustomerIdAsync([FromRoute] CustomerIdRequest Request)
    {
        return await CustomerService.FetchGuarantorByCustomerIdAsync(Request).ToResponseAsync();
    }

    /// <summary>
    ///     Obtener todos los prestamos del cliente
    /// </summary>
    /// <param name="Request">Envia el Id del Cliente</param>
    /// <returns>regres los prestamos del cliente</returns>
    [HttpGet("{CustomerId}/Loans")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<CustomerIdRequest>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<IAsyncEnumerable<LoanInfo>>> FetchLoansByCustomerIdAsync([FromRoute] CustomerIdRequest Request)
    {
        return await CustomerService.FetchLoanByCustomerIdAsync(Request).ToResponseAsync();
    }

    /// <summary>
    ///     Hace llamado al Servicio de CustomerService
    /// </summary>
    /// <param name="Request">Los Ids que vienen de la URL</param>
    /// <returns></returns>
    [HttpGet("{CustomerId}/Guarantor/{GuarantorId}")]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<GuarantorInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<GuarantorAndCustomerIdsRequest>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IResponse<GuarantorInfo?>>> FetchGuarantorByCustomerIdAndGuarantorIdAsync([FromRoute] GuarantorAndCustomerIdsRequest Request)
    {
        var Result = await CustomerService.FetchGuarantorByCustomerIdAndGuarantorIdAsync(Request);
        return StatusCode(StatusCodes.Status200OK, Result);
    }
}
