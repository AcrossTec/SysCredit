namespace SysCredit.Api.Validations.DNI.Nicaragua;

public class DniNicaraguaValidator : IDniValidator      
{
    public IEnumerable<string> Errors => throw new NotImplementedException();

    public IReadOnlyCollection<MunicipalityObject> Municipalities => throw new NotImplementedException();

    public bool IsValid(string Dni)
    {
        throw new NotImplementedException();
    }
}
