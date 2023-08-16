namespace SysCredit.Mobile.Models;

using CommunityToolkit.Mvvm.ComponentModel;

using SysCredit.Enums;

public partial class Reference : ModelBase
{
    [ObservableProperty]
    private long m_ReferenceId;

    [ObservableProperty]
    private string? m_Identification;

    [ObservableProperty]
    private string m_Name = string.Empty;

    [ObservableProperty]
    private string m_LastName = string.Empty;

    [ObservableProperty]
    private Gender m_Gender;

    [ObservableProperty]
    private string m_Phone = string.Empty;

    [ObservableProperty]
    private string? m_Email;

    [ObservableProperty]
    private string? m_Address;
}
