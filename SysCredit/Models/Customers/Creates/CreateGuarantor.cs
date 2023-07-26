﻿namespace SysCredit.Models.Customers.Creates;

using CommunityToolkit.Mvvm.ComponentModel;

using SysCredit.Validations;

using System.ComponentModel.DataAnnotations;

public partial class CreateGuarantor : ModelValidator
{
    [NotEmpty]
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

    public IEnumerable<string> IdentificationErrors => GetPropertyErrors(nameof(Identification));

    public bool IdentificationIsValid => !IdentificationErrors.Any();

    [NotEmpty]
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

    public IEnumerable<string> NameErrors => GetPropertyErrors(nameof(Name));

    public bool NameIsValid => !NameErrors.Any();

    [NotEmpty]
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

    public IEnumerable<string> LastNameErrors => GetPropertyErrors(nameof(LastName));

    public bool LastNameIsValid => !LastNameErrors.Any();

    [NotEmpty]
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

    public IEnumerable<string> AddressErrors => GetPropertyErrors(nameof(Address));

    public bool AddressIsValid => !AddressErrors.Any();

    [NotEmpty]
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

    public IEnumerable<string> NeighborhoodErrors => GetPropertyErrors(nameof(Neighborhood));

    public bool NeighborhoodIsValid => !NeighborhoodErrors.Any();

    [NotEmpty]
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

    public IEnumerable<string> BussinessTypeErrors => GetPropertyErrors(nameof(BussinessType));

    public bool BussinessTypeIsValid => !BussinessTypeErrors.Any();

    [NotEmpty]
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

    public IEnumerable<string> BussinessAddressErrors => GetPropertyErrors(nameof(BussinessAddress));

    public bool BussinessAddressIsValid => !BussinessAddressErrors.Any();

    [NotEmpty]
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

    public IEnumerable<string> PhoneErrors => GetPropertyErrors(nameof(Phone));

    public bool PhoneIsValid => !PhoneErrors.Any();

    [NotEmpty]
    [Display(Name = "Parentesco")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(RelationshipErrors))]
    [NotifyPropertyChangedFor(nameof(RelationshipIsValid))]
    [NotifyPropertyChangedFor(nameof(Errors))]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private Relationship? m_Relationship;

    public IEnumerable<string> RelationshipErrors => GetPropertyErrors(nameof(Relationship));

    public bool RelationshipIsValid => !RelationshipErrors.Any();
}
