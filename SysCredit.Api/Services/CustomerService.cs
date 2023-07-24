namespace SysCredit.Api.Services;

using SysCredit.Api.DataTransferObject;
using SysCredit.Api.Models;
using SysCredit.Api.Stores;
using SysCredit.Api.Validations;
using SysCredit.Api.ViewModels;

public interface ICustomerService
{
    ValueTask<Response<IAsyncEnumerable<CustomerDataTransferObject>>> GetCustomersAsync();

    ValueTask<Response<CustomerDataTransferObject>> AddCustomerAsync(CreateCustomer Customer);
}

public class CustomerService : ICustomerService
{
    private readonly IStore<Customer> CustomerStore;
    private readonly IStore<Guarantor> GuaratonStore;
    private readonly IStore<Relationship> RelationshipStore;

    public CustomerService(IStore<Entity> Store)
    {
        this.CustomerStore = Store.Store<Customer>();
        this.GuaratonStore = Store.Store<Guarantor>();
        this.RelationshipStore = Store.Store<Relationship>();
    }

    public ValueTask<Response<IAsyncEnumerable<CustomerDataTransferObject>>> GetCustomersAsync()
    {
        return ValueTask.FromResult(new Response<IAsyncEnumerable<CustomerDataTransferObject>>
        {
            Data = CustomerStore.GetCustomersAsync()
        });
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
        return new() { Data = Result };
    }
}
