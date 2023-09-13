namespace SysCredit.Api.Validations.DNI;

/// <summary>
///     Valida la fecha de la cedula de Nicaragua en base al limite minimo de edad que es 16 años de edad.
/// </summary>
public static class DNIExtension
{
    public static bool IsValidDate(this DateTime Date)
    {
        // Verificar que haya al menos 16 años de haber nacido para que la fecha sea válida
        DateTime Today = DateTime.Today;
        int Age = Today.Year - Date.Year;
        int MinimumAge = 16;

        if (Age < MinimumAge)
        {
            return false;
        }

        // Verificar que los días y meses sean correctos
        if (Date.Day < 1 || Date.Day > DateTime.DaysInMonth(Date.Year, Date.Month))
        {
            return false;
        }

        return true;
    }
}