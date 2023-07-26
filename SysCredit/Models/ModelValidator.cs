namespace SysCredit.Models;

using CommunityToolkit.Mvvm.ComponentModel;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public abstract class ModelValidator : ObservableValidator
{
    protected readonly ValidationContext ValidationContext;

    public ModelValidator()
    {
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
}
