namespace SysCredit.Mobile.Models;

using CommunityToolkit.Mvvm.ComponentModel;

public partial class Customer : ModelBase
{
    [ObservableProperty]
    private long m_CustomerId;

    [ObservableProperty]
    private string m_Identification = string.Empty;

    [ObservableProperty]
    private string m_Name = string.Empty;

    [ObservableProperty]
    private string m_LastName = string.Empty;

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
}
