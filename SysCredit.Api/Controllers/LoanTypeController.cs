namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;
using SysCredit.Api.ViewModels.LoanType;
using SysCredit.Api.ViewModels.LoanTypes;
using SysCredit.Helpers;

[ApiController]
[Route("Api/[controller]")]
public class LoanTypeController : ControllerBase
{
    private readonly ILoanTypeService LoanTypeService;

    public LoanTypeController(ILoanTypeService LoanTypeService)
    {
        this.LoanTypeService = LoanTypeService;
    }

    [HttpGet]
    public async Task<IResponse> FetchLoanTypeAsync()
    {
        return await LoanTypeService.FetchLoanTypeAsync().ToResponseAsync();
    }

    [HttpDelete("{LoanTypeId}")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteLoanTypeAsync(long? LoanTypeId)
    {
        var Result = await LoanTypeService.DeleteLoanTypeAsync(LoanTypeId);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(new DeleteLoanTypeRequest 
            { 
                LoanTypeId = LoanTypeId
            }));
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
            return StatusCode(StatusCodes.Status400BadRequest , await Result.ToResponseWithReplaceDataAsync(Request));
        }
        else
        {
            return StatusCode(StatusCodes.Status201Created, Result);
        }
    }

    [HttpGet("Complete")]
    public async Task<IResponse> FetchLoanTypeComplete()
    {
        return await LoanTypeService.FetchLoanTypeCompleteAsync().ToResponseAsync();
    }
}
