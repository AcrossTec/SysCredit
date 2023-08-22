namespace SysCredit.Mobile.Models.Customers.Creates;

using CommunityToolkit.Mvvm.ComponentModel;

using DynamicData.Binding;

using SysCredit.Enums;
using SysCredit.Mobile.Properties;
using SysCredit.Mobile.Validations;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public partial class CreateCustomer : ModelValidator
{
    protected override void SettingCollectionProperties()
    {
        AddNotifyPropertyChanged(() => References);
        AddNotifyPropertyChanged(() => Guarantors);
    }

    [Mandatory]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(IdentificationErrors))]
    [NotifyPropertyChangedFor(nameof(IdentificationIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLength(14, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(16, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [RegularExpression(@"\d{3}-?\d{6}-?\d{4}[A-Za-z]{1}", ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.RegexValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Identification))]
    private string m_Identification = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> IdentificationErrors => GetPropertyErrors(nameof(Identification));

    [JsonIgnore]
    public bool IdentificationIsValid => !IdentificationErrors.Any();


    [Mandatory]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(NameErrors))]
    [NotifyPropertyChangedFor(nameof(NameIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLength(2, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(64, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Name))]
    private string m_Name = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> NameErrors => GetPropertyErrors(nameof(Name));

    [JsonIgnore]
    public bool NameIsValid => !NameErrors.Any();


    [Mandatory]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(LastNameErrors))]
    [NotifyPropertyChangedFor(nameof(LastNameIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLength(2, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(64, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.LastName))]
    private string m_LastName = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> LastNameErrors => GetPropertyErrors(nameof(LastName));

    [JsonIgnore]
    public bool LastNameIsValid => !LastNameErrors.Any();


    [Enum<Gender>]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(GenderErrors))]
    [NotifyPropertyChangedFor(nameof(GenderIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [Required(ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.RequiredValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Gender))]
    private Gender? m_Gender;

    [JsonIgnore]
    public IEnumerable<string> GenderErrors => GetPropertyErrors(nameof(Gender));

    [JsonIgnore]
    public bool GenderIsValid => !GenderErrors.Any();


    [ObservableProperty]
    [NotifyDataErrorInfo]
    [EmailAddressIfNotEmpty]
    [NotifyPropertyChangedFor(nameof(EmailErrors))]
    [NotifyPropertyChangedFor(nameof(EmailIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLengthIfNotEmpty(10, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLengthIfNotEmpty(64, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
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
    [MinLength(2, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(256, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Address))]
    private string m_Address = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> AddressErrors => GetPropertyErrors(nameof(Address));

    [JsonIgnore]
    public bool AddressIsValid => !AddressErrors.Any();


    [Mandatory]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(NeighborhoodErrors))]
    [NotifyPropertyChangedFor(nameof(NeighborhoodIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLength(2, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(32, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Neighborhood))]
    private string m_Neighborhood = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> NeighborhoodErrors => GetPropertyErrors(nameof(Neighborhood));

    [JsonIgnore]
    public bool NeighborhoodIsValid => !NeighborhoodErrors.Any();


    [Mandatory]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(BussinessTypeErrors))]
    [NotifyPropertyChangedFor(nameof(BussinessTypeIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLength(2, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(32, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.BussinessType))]
    private string m_BussinessType = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> BussinessTypeErrors => GetPropertyErrors(nameof(BussinessType));

    [JsonIgnore]
    public bool BussinessTypeIsValid => !BussinessTypeErrors.Any();


    [Mandatory]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(BussinessAddressErrors))]
    [NotifyPropertyChangedFor(nameof(BussinessAddressIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [MinLength(2, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MinLengthValidationError))]
    [MaxLength(256, ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.MaxLengthValidationError))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.BussinessAddress))]
    private string m_BussinessAddress = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> BussinessAddressErrors => GetPropertyErrors(nameof(BussinessAddress));

    [JsonIgnore]
    public bool BussinessAddressIsValid => !BussinessAddressErrors.Any();


    [Mandatory]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(PhoneErrors))]
    [NotifyPropertyChangedFor(nameof(PhoneIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [RegularExpression(@"[578]{1}\d{7}", ErrorMessageResourceType = typeof(SysCreditResources), ErrorMessageResourceName = nameof(SysCreditResources.RegexValidationError))]
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
    [NotifyPropertyChangedFor(nameof(ReferencesErrors))]
    [NotifyPropertyChangedFor(nameof(ReferencesIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.References))]
    private IObservableCollection<CreateReference> m_References = new ObservableCollectionExtended<CreateReference>();

    [JsonIgnore]
    public IEnumerable<string> ReferencesErrors => GetPropertyErrors(nameof(References));

    [JsonIgnore]
    public bool ReferencesIsValid => !ReferencesErrors.Any();


    [Mandatory]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(GuarantorsErrors))]
    [NotifyPropertyChangedFor(nameof(GuarantorsIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    [Display(ResourceType = typeof(SysCreditResources), Name = nameof(SysCreditResources.Guarantors))]
    private IObservableCollection<Guarantor> m_Guarantors = new ObservableCollectionExtended<Guarantor>();

    [JsonIgnore]
    public IEnumerable<string> GuarantorsErrors => GetPropertyErrors(nameof(Guarantors));

    [JsonIgnore]
    public bool GuarantorsIsValid => !GuarantorsErrors.Any();
}
