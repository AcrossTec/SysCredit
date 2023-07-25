namespace SysCredit.Api.Services;

using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using SysCredit.Api.DataTransferObject;
using SysCredit.Api.Helpers.Extensions;
using SysCredit.Api.Helpers.Pagination;
using SysCredit.Api.Helpers.Search;
using SysCredit.Api.Helpers.Sorting;
using SysCredit.Api.Models;
using SysCredit.Api.Stores;
using SysCredit.Api.Validations.Customer;
using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Customer;

public interface ICustomerService
{
    ValueTask<Collection<CustomerDataTransferObject>> GetCustomersAsync(
        PagingOptions PaginOptions,
        SortOptions<CustomerOptions> SortOptions,
        SearchOptions<CustomerOptions> SearchOptions);

    ValueTask<Response<CustomerDataTransferObject>> AddCustomerAsync(CreateCustomer Customer);
}

public class CustomerService : ICustomerService
{
    private readonly IStore<Customer> CustomerStore;
    private readonly IStore<Guarantor> GuaratorStore;
    private readonly IStore<Reference> ReferenceStore;
    private readonly SysCreditOptions Options;

    public CustomerService(IStore<Entity> Store,
        IOptions<SysCreditOptions> Options)
    {
        this.CustomerStore = Store.Store<Customer>();
        this.GuaratorStore = Store.Store<Guarantor>();
        this.ReferenceStore = Store.Store<Reference>();
        this.Options = Options.Value;
    }


    public async ValueTask<Response<CustomerDataTransferObject>> AddCustomerAsync(CreateCustomer Customer)
    {
        var Validator = new CreateCustomerValidator();
        var ValidationResult = await Validator.ValidateAsync(Customer);
        
        if (!ValidationResult.IsValid)
        {
            return new()
            {
                Status = new ErrorStatus
                {
                    HasError = true,
                    ErrorCode = 400,
                    ErrorCategory = "ValidationError",
                    Message = "Error al validar los datos del cliente",
                    Errors = ValidationResult.ToDictionary()
                }
            };
        }

        var Result = await CustomerStore.AddCustomerAsync(Customer);

        // TODO: Validate this
        if (Customer.References.Count() > 0)
            await ReferenceStore.InsertMany(Customer.References, Enumerable.Repeat<long>(Result.CustomerId, Customer.References.Count()));

        if (Customer.Guarantors.Count() > 0)
            await GuaratorStore.InsertMany(Customer.Guarantors, Enumerable.Repeat<long>(Result.CustomerId, Customer.Guarantors.Count()));

        return new() { Value = Result.AsDto() };
    }

    public async ValueTask<Collection<CustomerDataTransferObject>> GetCustomersAsync(
        PagingOptions PagingOptions,
        SortOptions<CustomerOptions> SortOptions,
        SearchOptions<CustomerOptions> SearchOptions)
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

        var Customers = await CustomerStore.GetCustomersAsync(PagingOptions, Sort.Value , Search.Value);

        return new PagedCollection<CustomerDataTransferObject>()
        {
            Value = PagingOptions,
            Data = Customers.Items.AsListDto()
        };
    }
}
