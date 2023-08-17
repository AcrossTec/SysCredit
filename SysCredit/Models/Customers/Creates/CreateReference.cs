namespace SysCredit.Mobile.Models.Customers.Creates;

using CommunityToolkit.Mvvm.ComponentModel;

using SysCredit.Enums;
using SysCredit.Mobile.Validations;

using System.ComponentModel.DataAnnotations;

public partial class CreateReference : ModelValidator
{
    [MinLength(14)]
    [MaxLength(16)]
    [Display(Name = "Cédula")]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string? m_Identification;

    [Required]
    [MinLength(2)]
    [MaxLength(64)]
    [Display(Name = "Nombres")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(FullName))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string m_Name = string.Empty;

    [Required]
    [MinLength(2)]
    [MaxLength(64)]
    [Display(Name = "Apellidos")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(FullName))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string m_LastName = string.Empty;

    public string FullName => $"{Name} {LastName}";

    [Required]
    [Enum<Gender>]
    [Display(Name = "Género")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private Gender? m_Gender;

    [Required]
    [MinLength(8)]
    [MaxLength(16)]
    [Phone]
    [Display(Name = "Teléfono")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string m_Phone = string.Empty;

    [MinLength(10)]
    [MaxLength(64)]
    [EmailAddress]
    [Display(Name = "Correo")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string? m_Email;

    [MinLength(2)]
    [MaxLength(256)]
    [Display(Name = "Dirección")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string? m_Address;
}
