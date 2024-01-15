namespace SysCredit.Mobile.Models;

using CommunityToolkit.Mvvm.ComponentModel;

using SysCredit.Models;

public partial class Guarantor : ModelBase
{
    [ObservableProperty]
    private long m_GuarantorId;

    [ObservableProperty]
    private string m_Identification = string.Empty;

    [ObservableProperty]
    private string m_Name = string.Empty;

    [ObservableProperty]
    private string m_LastName = string.Empty;

    [ObservableProperty]
    private Gender m_Gender;

    [ObservableProperty]
    private string? m_Email;

    [ObservableProperty]
    private string m_Address = string.Empty;

    [ObservableProperty]
    private string m_Neighborhood = string.Empty;

    [ObservableProperty]
    private string m_BussinessType = string.Empty;

    [ObservableProperty]
    private string m_BussinessAddress = string.Empty;

    [ObservableProperty]
    private string m_Phone = string.Empty;

    [ObservableProperty]
    private Relationship m_Relationship = new();
}
