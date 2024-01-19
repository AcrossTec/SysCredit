namespace SysCredit.Mobile.Mockup;

using DynamicData.Binding;
using SysCredit.Helpers;
using SysCredit.Mobile.Models;
using SysCredit.Mobile.Models.Customers.Creates;

public class CustomerRepository
{
    private static List<CreateCustomer> Customers = new();

    public static async Task<Response> AddCustomer(CreateCustomer request)
    {
        await Task.Delay(100);

        var result = ValidateCustomer(request);

        if (result != null)
        {
            return new Response
            {
                Status =
                {
                    HasError = true,
                    ErrorMessage = result
                }
            };
        }
        else
        {
            request.Guarantors.Add(new Guarantor
            {
                GuarantorId = 1,
                Identification = "0812233001000A",
                Name = "Juan",
                LastName = "Torres",
                Gender = SysCredit.Models.Gender.Male,
                Email = "juan@email.com",
                Address = "Calle 1 # 1 - 1",
                Neighborhood = "Barrio 1",
                BussinessType = "Tienda",
                BussinessAddress = "Calle 1 # 1 - 1",
                Phone = "3001234567",
                Relationship = new Relationship()
                {
                    RelationshipId = 1,
                    Name = "Padre"
                }
            });

            Customers.Add(request);

            return new Response
            {
                Data   = Customers.Count,
                Status =
                {
                    HasError = false,
                }
            };
        }
    }

    public static string? ValidateCustomer(CreateCustomer model)
    {
        if (Customers.FirstOrDefault(x => x.Identification == model.Identification) != null)
            return "El cliente ya existe";

        if (Customers.FirstOrDefault(x => x.Phone == model.Phone) != null)
            return "El número de teléfono ya existe";

        if (Customers.FirstOrDefault(x => x.Email == model.Email) != null)
            return "El correo electrónico ya existe";

        //if (model.Guarantors.Count == 0)
        //    return "Debe agregar al menos un garante";

        if (model.References.Count == 0)
            return "Debe agregar al menos una referencia";

        return null;
    }
}
