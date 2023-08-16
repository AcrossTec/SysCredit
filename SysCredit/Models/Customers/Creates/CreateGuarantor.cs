namespace SysCredit.Mobile.Models.Customers.Creates;

using CommunityToolkit.Mvvm.ComponentModel;

using SysCredit.Enums;
using SysCredit.Mobile.Validations;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public partial class CreateGuarantor : ModelValidator
{
    [Mandatory]
    [MinLength(14)]
    [MaxLength(16)]
    [RegularExpression(@"\d{3}-?\d{6}-?\d{4}[A-Za-z]{1}")]
    [Display(Name = "Cédula")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(IdentificationErrors))]
    [NotifyPropertyChangedFor(nameof(IdentificationIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string m_Identification = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> IdentificationErrors => GetPropertyErrors(nameof(Identification));

    [JsonIgnore]
    public bool IdentificationIsValid => !IdentificationErrors.Any();

    [Mandatory]
    [MinLength(2)]
    [MaxLength(64)]
    [Display(Name = "Nombres")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(NameErrors))]
    [NotifyPropertyChangedFor(nameof(NameIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string m_Name = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> NameErrors => GetPropertyErrors(nameof(Name));

    [JsonIgnore]
    public bool NameIsValid => !NameErrors.Any();

    [Mandatory]
    [MinLength(2)]
    [MaxLength(64)]
    [Display(Name = "Apellidos")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(LastNameErrors))]
    [NotifyPropertyChangedFor(nameof(LastNameIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string m_LastName = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> LastNameErrors => GetPropertyErrors(nameof(LastName));

    [JsonIgnore]
    public bool LastNameIsValid => !LastNameErrors.Any();

    [EmailAddress]
    [MaxLength(64)]
    [Display(Name = "Correo")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(EmailErrors))]
    [NotifyPropertyChangedFor(nameof(EmailIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string m_Email = string.Empty;


    [Required]
    [Enum<Gender>]
    [Display(Name = "Género")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(GenderErrors))]
    [NotifyPropertyChangedFor(nameof(GenderIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private Gender? m_Gender;

    [JsonIgnore]
    public IEnumerable<string> GenderErrors => GetPropertyErrors(nameof(Gender));

    [JsonIgnore]
    public bool GenderIsValid => !GenderErrors.Any();


    [JsonIgnore]
    public IEnumerable<string> EmailErrors => GetPropertyErrors(nameof(Email));

    [JsonIgnore]
    public bool EmailIsValid => !EmailErrors.Any();

    [Mandatory]
    [MinLength(2)]
    [MaxLength(256)]
    [Display(Name = "Dirección")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(AddressErrors))]
    [NotifyPropertyChangedFor(nameof(AddressIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string m_Address = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> AddressErrors => GetPropertyErrors(nameof(Address));

    [JsonIgnore]
    public bool AddressIsValid => !AddressErrors.Any();

    [Mandatory]
    [MinLength(2)]
    [MaxLength(32)]
    [Display(Name = "Barrio")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(NeighborhoodErrors))]
    [NotifyPropertyChangedFor(nameof(NeighborhoodIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string m_Neighborhood = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> NeighborhoodErrors => GetPropertyErrors(nameof(Neighborhood));

    [JsonIgnore]
    public bool NeighborhoodIsValid => !NeighborhoodErrors.Any();

    [Mandatory]
    [MinLength(2)]
    [MaxLength(32)]
    [Display(Name = "Tipo de Negocio")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(BussinessTypeErrors))]
    [NotifyPropertyChangedFor(nameof(BussinessTypeIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string m_BussinessType = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> BussinessTypeErrors => GetPropertyErrors(nameof(BussinessType));

    [JsonIgnore]
    public bool BussinessTypeIsValid => !BussinessTypeErrors.Any();

    [Mandatory]
    [MinLength(2)]
    [MaxLength(256)]
    [Display(Name = "Dirección del Negocio")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(BussinessAddressErrors))]
    [NotifyPropertyChangedFor(nameof(BussinessAddressIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string m_BussinessAddress = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> BussinessAddressErrors => GetPropertyErrors(nameof(BussinessAddress));

    [JsonIgnore]
    public bool BussinessAddressIsValid => !BussinessAddressErrors.Any();

    [Mandatory]
    [MinLength(8)]
    [MaxLength(16)]
    [RegularExpression(@"[578]{1}\d{7}")]
    [Display(Name = "Dirección del Negocio")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(PhoneErrors))]
    [NotifyPropertyChangedFor(nameof(PhoneIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string m_Phone = string.Empty;

    [JsonIgnore]
    public IEnumerable<string> PhoneErrors => GetPropertyErrors(nameof(Phone));

    [JsonIgnore]
    public bool PhoneIsValid => !PhoneErrors.Any();

    [Mandatory]
    [Display(Name = "Parentesco")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(RelationshipErrors))]
    [NotifyPropertyChangedFor(nameof(RelationshipIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private Relationship? m_Relationship;

    [JsonIgnore]
    public IEnumerable<string> RelationshipErrors => GetPropertyErrors(nameof(Relationship));

    [JsonIgnore]
    public bool RelationshipIsValid => !RelationshipErrors.Any();
}
