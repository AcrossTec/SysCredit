using Microsoft.Extensions.Options;
using SysCredit.Api.DataTransferObject;
using SysCredit.Api.Helpers.Extensions;
using SysCredit.Api.Helpers.Pagination;
using SysCredit.Api.Helpers.Search;
using SysCredit.Api.Helpers.Sorting;
using SysCredit.Api.Models;
using SysCredit.Api.Stores;
using SysCredit.Api.Validations.Guarantor;
using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Guarantor;

namespace SysCredit.Api.Services;

public interface IGuarantorServices
{
    ValueTask<Collection<GuarantorDataTransferObject>> GetGuarantorsAsync(
        PagingOptions PagingOptions,
        SortOptions<GuarantorOption> SortOptions,
        SearchOptions<GuarantorOption> SearchOptions);

    ValueTask<Response<GuarantorDataTransferObject>> AddGuarantorAsync(CreateGuarantor Customer);
}
public class GurantorServices : IGuarantorServices
{
    private readonly IStore<Guarantor> GuarantorStore;
    private readonly SysCreditOptions Options;

    public GurantorServices(IStore<Guarantor> GuarantorStore,
        IOptions<SysCreditOptions> Options)
    {
        this.GuarantorStore = GuarantorStore;
        this.Options = Options.Value;
    }

    public async ValueTask<Response<GuarantorDataTransferObject>> AddGuarantorAsync(CreateGuarantor Guarantor)
    {
        var Validator = new CreateGuarantorValidator();
        var ValidationResult = await Validator.ValidateAsync(Guarantor);

        if (!ValidationResult.IsValid)
        {
            return new()
            {
                Status = new ErrorStatus
                {
                    HasError = true,
                    ErrorCode = 400,
                    ErrorCategory = "ValidationError",
                    Message = "Error al validar los datos del avalista",
                    Errors = ValidationResult.ToDictionary()
                }
            };
        }

        var Result = await GuarantorStore.AddGuarantorAsync(Guarantor);
        return new() { Value = Result.AsDto() };
    }

    public async ValueTask<Collection<GuarantorDataTransferObject>> GetGuarantorsAsync(
        PagingOptions PagingOptions,
        SortOptions<GuarantorOption> SortOptions,
        SearchOptions<GuarantorOption> SearchOptions)
    {
        PagingOptions.Offset = PagingOptions.Offset ?? Options.PagingOptions.Offset;
        PagingOptions.Limit = PagingOptions.Limit ?? Options.PagingOptions.Limit;

        var Sort = SortOptions.Processor().Apply();
        var Search = SearchOptions.Processor().Apply();

        if (!Sort.IsValid)
        {
            return new()
            {
                Status = new ErrorStatus
                {
                    HasError = true,
                    ErrorCode = 400,
                    ErrorCategory = "ValidationError",
                    Message = Sort.Value,
                }
            };
        }

        if (!Search.IsValid)
        {
            return new()
            {
                Status = new ErrorStatus
                {
                    HasError = true,
                    ErrorCode = 400,
                    ErrorCategory = "ValidationError",
                    Message = Search.Value,
                }
            };
        }

        var Guarantors = await GuarantorStore.GetGuarantorsAsync(PagingOptions, Sort.Value, Search.Value);

        return new PagedCollection<GuarantorDataTransferObject>()
        {
            Value = PagingOptions,
            Data = Guarantors.Items.AsListDto()
        };
    }
}
