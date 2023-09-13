namespace SysCredit.Api.Validations.DNI.Nicaragua;

using System.Text.RegularExpressions;

public class DniNicaraguaValidator : IDniValidator      
{
    public string Errors { get; set; } = String.Empty;

    public bool IsValid(string? Dni)
    {
        if (Dni == null)
        {
            return false;
        }

        // Expresion regular para validar el DNI
        string RegexPattern = @"^([0-6][\d]{2})-([0-2][\d]|3[0-1])(0[1-9]|1[0-2])([\d]{2})-([\d]{4}[A-X])$";
        Regex Regex = new Regex(RegexPattern);

        // Comprueba si el DNI coincide con el formato esperado
        if (!Regex.IsMatch(Dni))
        {
            this.Errors =  "Error, el formato no es el correcto";
            return false;
        }

        // Extrayendo el código de Municipio del DNI
        string MunicipalityCode = Dni.Substring(0, 3);

        // Si no se encuentra el código de Municipio, el DNI no es válido
        if (!Municipalities.MunicipalitiesList.ContainsKey(MunicipalityCode))
        {
            this.Errors = "Error, el codigo del municipio no existe";
            return false;
        }

        // Extrayendo el apartado de fecha del DNI
        string DateSection = Dni.Split('-')[1];
        int Day = int.Parse(DateSection.Substring(0, 2));
        int Month = int.Parse(DateSection.Substring(2, 2));

        int NewMillenium = 2000;
        int PastMillennium = 1900;
        int CurrentYear = DateTime.Today.Year - NewMillenium;
        int Year = int.Parse(DateSection.Substring(4, 2));

        if (CurrentYear >  Year) 
        {
            Year += NewMillenium;
        }
        else if (CurrentYear < Year) 
        {
            Year += PastMillennium;
        }

        DateTime Date = new DateTime(Year, Month, Day);

        // Check if the date is a valid date
        bool ResultOfValidateDate = Date.IsValidDate();

        if (!ResultOfValidateDate)
        {
            this.Errors = "Error, la fecha de nacimiento no es valida";
            return false;
        }

        return true;
    }
}