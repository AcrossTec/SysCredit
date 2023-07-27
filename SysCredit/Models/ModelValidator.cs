namespace SysCredit.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[ObservableRecipient]
public abstract partial class ModelValidator : ObservableValidator
{
    protected readonly ValidationContext ValidationContext;

    public ModelValidator()
    {
        Messenger = WeakReferenceMessenger.Default;
        ValidationContext = new ValidationContext(this);
    }

    public virtual bool IsValid => Validator.TryValidateObject(this, ValidationContext, null, true);

    public IEnumerable<string> Errors
    {
        get
        {
            var LocalErrors = new List<ValidationResult>();
            Validator.TryValidateObject(this, ValidationContext, LocalErrors, true);
            return LocalErrors.Select(Error => Error.ErrorMessage!);
        }
    }

    public IEnumerable<string> GetPropertyErrors(string? PropertyName = null)
        => GetErrors(PropertyName).Select(Error => Error.ErrorMessage!);

    public void ClearPropertyErrors(string? PropertyName = null)
        => ClearErrors(PropertyName);
}
