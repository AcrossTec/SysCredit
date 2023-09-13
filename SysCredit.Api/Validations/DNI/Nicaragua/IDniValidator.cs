namespace SysCredit.Api.Validations.DNI.Nicaragua;

using System.Collections.Generic;

public interface IDniValidator
{
    bool IsValid(string Dni);

    IEnumerable<string> Errors { get; }

    IReadOnlyCollection<MunicipalityObject> Municipalities { get; }
}
