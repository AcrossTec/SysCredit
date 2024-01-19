namespace SysCredit.Mobile.Models.Customers.Creates;

using CommunityToolkit.Mvvm.ComponentModel;

using SysCredit.Mobile.Properties;
using SysCredit.Mobile.Validations;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public partial class CreateReference : ModelValidator
{
    [Mandatory]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(IdentificationErrors))]
    [NotifyPropertyChangedFor(nameof(IdentificationIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLengthIfNotEmpty(14, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLengthIfNotEmpty(16, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [RegularExpression(@"\d{3}-?\d{6}-?\d{4}[A-Za-z]{1}", ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.Validation_IdentificationFormatError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Identification))]
    private string m_Identification = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> IdentificationErrors => GetPropertyErrors(nameof(Identification));

    [JsonIgnore]
    public bool IdentificationIsValid => !IdentificationErrors.Any();


    [ObservableProperty]
    [NotifyDataErrorInfo]
    [FieldMustContainsOnlyLetters]
    [NotifyPropertyChangedFor(nameof(NameErrors))]
    [NotifyPropertyChangedFor(nameof(NameIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [Required(ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.RequiredValidationError))]
    [MinLength(2, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(64, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Name))]
    private string m_Name = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> NameErrors => GetPropertyErrors(nameof(Name));

    [JsonIgnore]
    public bool NameIsValid => !NameErrors.Any();


    [ObservableProperty]
    [NotifyDataErrorInfo]
    [FieldMustContainsOnlyLetters]
    [NotifyPropertyChangedFor(nameof(LastNameErrors))]
    [NotifyPropertyChangedFor(nameof(LastNameIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [Required(ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.RequiredValidationError))]
    [MinLength(2, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(64, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.LastName))]
    private string m_LastName = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> LastNameErrors => GetPropertyErrors(nameof(LastName));

    [JsonIgnore]
    public bool LastNameIsValid => !LastNameErrors.Any();


    [Enum<SysCredit.Models.Gender>]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(GenderErrors))]
    [NotifyPropertyChangedFor(nameof(GenderIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [Required(ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.RequiredValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Gender))]
    private SysCredit.Models.Gender? m_Gender;

    [JsonIgnore]
    public IEnumerable<string> GenderErrors => GetPropertyErrors(nameof(Gender));

    [JsonIgnore]
    public bool GenderIsValid => !GenderErrors.Any();


    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(PhoneErrors))]
    [NotifyPropertyChangedFor(nameof(PhoneIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [RegularExpression(@"[578]{1}\d+", ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.Validation_PhoneFormatError))]
    [Required(ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.RequiredValidationError))]
    [MinLength(8, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(16, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Phone))]
    private string m_Phone = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> PhoneErrors => GetPropertyErrors(nameof(Phone));

    [JsonIgnore]
    public bool PhoneIsValid => !PhoneErrors.Any();

    [Mandatory]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    //[EmailAddressIfNotEmpty]
    [NotifyPropertyChangedFor(nameof(EmailErrors))]
    [NotifyPropertyChangedFor(nameof(EmailIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLengthIfNotEmpty(10, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLengthIfNotEmpty(64, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [RegularExpression(@"[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}", ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.Validation_EmailFormatError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Email))]
    private string m_Email = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> EmailErrors => GetPropertyErrors(nameof(Email));

    [JsonIgnore]
    public bool EmailIsValid => !EmailErrors.Any();

    [Mandatory]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(AddressErrors))]
    [NotifyPropertyChangedFor(nameof(AddressIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLengthIfNotEmpty(2, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLengthIfNotEmpty(256, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Address))]
    private string? m_Address;

    [JsonIgnore]
    public IEnumerable<string> AddressErrors => GetPropertyErrors(nameof(Address));

    [JsonIgnore]
    public bool AddressIsValid => !AddressErrors.Any();
}
