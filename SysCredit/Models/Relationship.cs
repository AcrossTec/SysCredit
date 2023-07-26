namespace SysCredit.Models;

using CommunityToolkit.Mvvm.ComponentModel;

public partial class Relationship : ModelBase
{
    [ObservableProperty]
    private long m_RelationshipId;

    [ObservableProperty]
    private string m_Name = string.Empty;
}
