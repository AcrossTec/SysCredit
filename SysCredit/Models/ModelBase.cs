namespace SysCredit.Mobile.Models;

using CommunityToolkit.Mvvm.ComponentModel;

public partial class ModelBase : ObservableObject
{
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool IsEdit { get; set; }

    public bool IsDelete { get; set; }
}
