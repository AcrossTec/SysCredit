namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;
using SysCredit.Api.ViewModels.LoanType;
using SysCredit.Api.ViewModels.LoanTypes;
using SysCredit.Helpers;

/// <summary>
/// 
/// </summary>
/// <param name="LoanTypeService"></param>
[ApiController]
[Route("Api/[controller]")]
public class LoanTypeController(ILoanTypeService LoanTypeService) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanTypeInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchLoanTypeAsync()
    {
        return await LoanTypeService.FetchLoanTypeAsync().ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("Complete")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanTypeInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchLoanTypeComplete()
    {
        return await LoanTypeService.FetchLoanTypeCompleteAsync().ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpDelete("{LoanTypeId}")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(IResponse<DeleteLoanTypeRequest>))]
    public async Task<IActionResult> DeleteLoanTypeAsync([FromRoute] DeleteLoanTypeRequest Request)
    {
        var Result = await LoanTypeService.DeleteLoanTypeAsync(Request);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(Request));
        }
        else
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(IResponse<CreateLoanTypeRequest>))]
    public async Task<IActionResult> InsertLoanTypeAsync([FromBody] CreateLoanTypeRequest Request)
    {
        var Result = await LoanTypeService.InsertLoanTypeAsync(Request);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(Request));
        }
        else
        {
            return StatusCode(StatusCodes.Status201Created, Result);
        }
    }

    [HttpPut("{LoanTypeId}")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(IResponse<UpdateLoanTypeRequest>))]
    public async Task<IActionResult> UpdateLaontypeAsync([FromBody] UpdateLoanTypeRequest Request, long? LoanTypeId)
    {
        if (Request.LoanTypeId == LoanTypeId)
        {
            var Result = await LoanTypeService.UpdateLoanTypeAsync(Request);

            if (Result.Status.HasError)
            {
                return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(Request));
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent, Result);
            }
        }
        else
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
