using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysCredit.Mobile.Models.Customers.Creates;
using SysCredit.Mobile.Models;
using SysCredit.Models;
using Guarantor = SysCredit.Mobile.Models.Guarantor;

namespace SysCredit.Mobile.Mockup;

public class GuarantorRepository
{
    public static List<CreateGuarantor> GuarantorsList = new List<CreateGuarantor>();

    public static void AddGuarantor(CreateGuarantor Model)
    {
        GuarantorsList.Add(Model);
    }

    public static string? ValidateGuarantor(CreateGuarantor Model)
    {
        if (GuarantorsList.FirstOrDefault(g => g.Identification == Model.Identification) != null)
        {
            return "La cédula ya existe";
        }
        if (GuarantorsList.FirstOrDefault(g => g.Email == Model.Email) != null)
        {
            return "El correo ya existe";
        }
        if (GuarantorsList.FirstOrDefault(g => g.Phone == Model.Phone) != null)
        {
            return "El teléfono ya existe";
        }
        return null;
    }

    public static List<Guarantor> SearchGuarantors(string? Query = null, int? Offset = null, int? Limit = null)
    {
        var Guarantors = GuarantorsList.AsQueryable();

        if (!string.IsNullOrEmpty(Query))
        {
            Guarantors = Guarantors.Where(g => g.Name.Contains(Query) || g.Identification.Contains(Query) || g.Email.Contains(Query));
        }

        //if (Offset.HasValue)
        //{
        //    Guarantors = Guarantors.Skip(Offset.Value);
        //}

        //if (Limit.HasValue)
        //{
        //    Guarantors = Guarantors.Take(Limit.Value);
        //}

        return Guarantors.Select(g => new Guarantor
        {
            Identification = g.Identification,
            Name = g.Name,
            LastName = g.LastName,
            Gender = g.Gender ?? default,
            Email = g.Email,
            Address = g.Address,
            Neighborhood = g.Neighborhood,
            BussinessType = g.BussinessType,
            BussinessAddress = g.BussinessAddress,
            Phone = g.Phone,
            Relationship = g.Relationship! 
        }).ToList();
    }

}
