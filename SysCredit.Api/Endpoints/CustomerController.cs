namespace SysCredit.Api.Endpoints;

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
public class CustomerController(ICustomerManager CustomerService, ILogger<CustomerController> Logger) : ControllerBase
{
    /// <summary>
    ///     Obtiene todos clientes de la tabla <see cref="Models.Customer"/>.
    /// </summary>
    /// <returns>
    ///     Regresa todos los clientes de la tabla <see cref="Models.Customer"/>.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<CustomerInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<IAsyncEnumerable<CustomerInfo>>>> FetchCustomersAsync()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Customer");
        return Ok(await CustomerService.FetchCustomerAsync().ToResponseAsync());
    }

    /// <summary>
    ///     Obtiene los <see cref="Models.Customer"/> según el criterio de búsqueda.  
    /// </summary>
    /// <param name="Request">
    ///     Dato del criterio de búsqueda.
    /// </param>
    /// <returns>
    ///     Retorna los registros que corresponden al criterio de búsqueda.
    /// </returns>
    [HttpGet("Search")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<SearchCustomer>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<IAsyncEnumerable<SearchCustomer>>>> SearchCustomerAsync([FromQuery] SearchRequest Request)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Customer/Search");
        return Ok(await CustomerService.SearchCustomerAsync(Request).ToResponseAsync());
    }

    /// <summary>
    ///     Obtiene un registro de la tabla <see cref="Models.Customer"/>.
    /// </summary>
    /// <param name="CustomerId">
    ///     Id del cliente obtenido de la ruta.
    /// </param>
    /// <returns>
    ///     Regresa los datos del cliente.
    /// </returns>
    [HttpGet("{CustomerId}")]
    [ProducesResponseType(typeof(IResponse<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<CustomerInfo>>> FetchCustomerByIdAsync(long? CustomerId)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Customer/{CustomerId}", CustomerId);
        return Ok(await CustomerService.FetchCustomerByIdAsync(CustomerId).ToResponseAsync());
    }

    /// <summary>
    ///     Obtiene un cliente que corresponde al documento de identificación.
    /// </summary>
    /// <param name="Identification">
    ///     Documento de identidad del registro del cliente que se buscará.
    /// </param>
    /// <returns>
    ///     Retorna los datos del cliente.
    /// </returns>
    [HttpGet("ByIdentification/{Identification}")]
    [ProducesResponseType(typeof(IResponse<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<CustomerInfo>>> FetchCustomerByIdentificationAsync(string? Identification)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Customer/ByIdentification/{Identification}", Identification);
        return Ok(await CustomerService.FetchCustomerByIdentificationAsync(Identification).ToResponseAsync());
    }

    /// <summary>
    ///     Obtiene un cliente por su correo.
    /// </summary>
    /// <param name="Email">
    ///     Correo electrónico del registro del cliente que se buscará.
    /// </param>
    /// <returns>
    ///     Retorna los datos del cliente.
    /// </returns>
    [HttpGet("ByEmail/{Email}")]
    [ProducesResponseType(typeof(IResponse<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<CustomerInfo>>> FetchCustomerByEmailAsync(string? Email)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Customer/ByEmail/{Email}", Email);
        return Ok(await CustomerService.FetchCustomerByEmailAsync(Email).ToResponseAsync());
    }

    /// <summary>
    ///     Obtiene un cliente por su teléfono.
    /// </summary>
    /// <param name="Phone">
    ///     Teléfono del registro de cliente que se buscará.
    /// </param>
    /// <returns>
    ///     Retorna los datos del cliente.
    /// </returns>
    [HttpGet("ByPhone/{Phone}")]
    [ProducesResponseType(typeof(IResponse<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<CustomerInfo>>> FetchCustomerByPhoneAsync(string? Phone)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Customer/ByPhone/{Phone}", Phone);
        return Ok(await CustomerService.FetchCustomerByPhoneAsync(Phone).ToResponseAsync());
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
        Logger.LogInformation("EndPoint[POST]: /Api/Customer");
        var ServiceResult = await CustomerService.InsertCustomerAsync(Request);
        return StatusCode(StatusCodes.Status201Created, await ServiceResult.ToResponseAsync());
    }

    /// <summary>
    ///     Obtiene todos las referencias de un cliente.
    /// </summary>
    /// <param name="Request">
    ///     Se envía el Id del cliente.
    /// </param>
    /// <returns>
    ///     Regresa una lista de referencias del cliente.
    /// </returns>
    [HttpGet("{CustomerId}/References")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<ReferenceInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<CustomerIdRequest>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<IAsyncEnumerable<ReferenceInfo>>> FetchReferencesByCustomerIdAsync([FromRoute] CustomerIdRequest Request)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Customer/{CustomerId}/References", Request.CustomerId);
        return await CustomerService.FetchReferenceByCustomerIdAsync(Request).ToResponseAsync();
    }

    /// <summary>
    ///     Obtiene todos los fiadores de un cliente.
    /// </summary>
    /// <param name="Request">
    ///     Se envía el Id del cliente.
    /// </param>
    /// <returns>
    ///     Regresa una lista de fiadores del cliente.
    /// </returns>
    [HttpGet("{CustomerId}/Guarantors")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<ReferenceInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<CustomerIdRequest>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<IAsyncEnumerable<GuarantorInfo>>> FetchGuarantorsByCustomerIdAsync([FromRoute] CustomerIdRequest Request)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Customer/{CustomerId}/Guarantors", Request.CustomerId);
        return await CustomerService.FetchGuarantorByCustomerIdAsync(Request).ToResponseAsync();
    }

    /// <summary>
    ///     Obtiene todos los prestamos del cliente.
    /// </summary>
    /// <param name="Request">
    ///     Se envía el Id del cliente.
    /// </param>
    /// <returns>
    ///     Regresa una lista de préstamos de cliente.
    /// </returns>
    [HttpGet("{CustomerId}/Loans")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<CustomerIdRequest>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<IAsyncEnumerable<LoanInfo>>> FetchLoansByCustomerIdAsync([FromRoute] CustomerIdRequest Request)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Customer/{CustomerId}/Loans", Request.CustomerId);
        return await CustomerService.FetchLoanByCustomerIdAsync(Request).ToResponseAsync();
    }

    /// <summary>
    ///     Obtiene un fiador según su Id con respecto a un cliente.
    /// </summary>
    /// <param name="Request">
    ///     Los Ids que vienen de la URL.
    /// </param>
    /// <returns>
    ///     Regresa los datos del fiador.
    /// </returns>
    [HttpGet("{CustomerId}/Guarantor/{GuarantorId}")]
    [ProducesResponseType(typeof(IResponse<GuarantorInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<GuarantorAndCustomerIdsRequest>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<GuarantorInfo?>>> FetchGuarantorByCustomerIdAndGuarantorIdAsync([FromRoute] GuarantorAndCustomerIdsRequest Request)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Customer/{CustomerId}/Guarantor/{GuarantorId}", Request.CustomerId, Request.GuarantorId);
        var Result = await CustomerService.FetchGuarantorByCustomerIdAndGuarantorIdAsync(Request);
        return StatusCode(StatusCodes.Status200OK, Result);
    }
}
