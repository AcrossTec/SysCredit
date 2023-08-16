namespace SysCredit.Mobile.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using DynamicData.Binding;

using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

[ObservableRecipient]
public abstract partial class ModelValidator : ObservableValidator
{
    protected readonly ValidationContext ValidationContext;

    public ModelValidator()
    {
        Messenger = WeakReferenceMessenger.Default;
        ValidationContext = new ValidationContext(this);
        SettingComplexProperties();
        SettingCollectionProperties();
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

    protected virtual void SettingComplexProperties()
    {
    }

    protected virtual void SettingCollectionProperties()
    {
    }

    protected void AddNotifyPropertyChanged<T>(Expression<Func<T?>> Expression) where T : INotifyPropertyChanged
    {
        MemberExpression ExpressionBody = (MemberExpression)Expression.Body;
        string PropertyName = ExpressionBody.Member.Name;

        switch (Expression.Compile()())
        {
            case INotifyCollectionChanged Collection:
            {
                Collection.CollectionChanged += delegate
                {
                    OnPropertyChanged(PropertyName);
                    OnPropertyChanged(nameof(Errors));
                    OnPropertyChanged(nameof(IsValid));
                };
                break;
            }
            case INotifyPropertyChanged Property:
            {
                Property.PropertyChanged += delegate
                {
                    OnPropertyChanged(PropertyName);
                    OnPropertyChanged(nameof(Errors));
                    OnPropertyChanged(nameof(IsValid));
                };
                break;
            }
        }
    }
}
