namespace SysCredit.Mobile.Models.Customers.Creates;

using CommunityToolkit.Mvvm.ComponentModel;

using SysCredit.Mobile.Properties;
using SysCredit.Mobile.Validations;

using System.ComponentModel.DataAnnotations;

public partial class CreateReference : ModelValidator
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLengthIfNotEmpty(14, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLengthIfNotEmpty(16, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Identification))]
    private string? m_Identification;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [Required(ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.RequiredValidationError))]
    [MinLength(2, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(64, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Name))]
    private string m_Name = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [Required(ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.RequiredValidationError))]
    [MinLength(2, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(64, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.LastName))]
    private string m_LastName = string.Empty;

    [Enum<SysCredit.Models.Gender>]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [Required(ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.RequiredValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Gender))]
    private SysCredit.Models.Gender? m_Gender;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [Phone(ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.PhoneValidationError))]
    [Required(ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.RequiredValidationError))]
    [MinLength(8, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(16, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Phone))]
    private string m_Phone = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [EmailAddressIfNotEmpty]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLengthIfNotEmpty(10, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLengthIfNotEmpty(64, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Email))]
    private string? m_Email;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLengthIfNotEmpty(2, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLengthIfNotEmpty(256, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Address))]
    private string? m_Address;
}
