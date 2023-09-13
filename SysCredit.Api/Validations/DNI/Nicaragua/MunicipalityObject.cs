namespace SysCredit.Api.Validations.DNI.Nicaragua;

public class MunicipalityObject
{
    public int MunicipalityCode { get; set; } 

    public String MunicipalityName { get; set; }

    public MunicipalityObject(int MunicipalityCode, string MunicipalityName) 
    {
        this.MunicipalityCode = MunicipalityCode;
        this.MunicipalityName = MunicipalityName;
    }
}
