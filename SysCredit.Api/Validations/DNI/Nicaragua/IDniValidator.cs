namespace SysCredit.Api.Validations.DNI.Nicaragua;

using System.Collections.Generic;

public interface IDniValidator
{
    bool IsValid(string Dni);

    public string Errors { get; }
}
