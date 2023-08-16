namespace SysCredit.Mobile.Behaviors;

using Microsoft.Maui.Controls;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using UraniumUI.Material.Controls;
using UraniumUI.Validations;

public class ValidationBehavior : Behavior<InputField>
{
    public static readonly BindableProperty ModelTypeProperty = BindableProperty.Create(nameof(ModelType), typeof(Type), typeof(ValidationBehavior));
    public static readonly BindableProperty PathProperty = BindableProperty.Create(nameof(Path), typeof(string), typeof(ValidationBehavior));

    public Type ModelType
    {
        get => (Type)GetValue(ModelTypeProperty);
        set => SetValue(ModelTypeProperty, value);
    }

    public string Path
    {
        get => (string)GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }

    protected override void OnAttachedTo(InputField Input)
    {
        if (!IsSet(ModelTypeProperty))
            throw new ArgumentNullException(nameof(ModelType));

        if (!IsSet(PathProperty))
            throw new ArgumentNullException(nameof(Path));

        var PropertyInfo = GetProperty(ModelType, Path);

        if (PropertyInfo is null)
        {
            throw new InvalidOperationException($"No se encuentra la propiedad: '{Path}'.");
        }

        var Attributes = PropertyInfo.GetCustomAttributes<ValidationAttribute>(true);
        var DisplayAttribute = PropertyInfo.GetCustomAttribute<DisplayAttribute>(true);

        foreach (var Attribute in Attributes)
            Input.Validations.Add(new DataAnnotationValidation(Attribute, DisplayAttribute?.GetName() ?? PropertyInfo.Name));
    }

    protected override void OnDetachingFrom(InputField Input)
    {
        Input.Validations.RemoveAll(Validation => Validation is DataAnnotationValidation);
    }

    protected PropertyInfo? GetProperty(Type Type, string PropertyName)
    {
        if (!IsNestedProperty(PropertyName))
        {
            return Type.GetProperty(PropertyName);
        }

        var Splitted = PropertyName.Split('.');
        var Property = Type.GetProperty(Splitted[0]);

        if (Property is null)
        {
            return Property;
        }

        return GetProperty(Property.PropertyType, string.Join('.', Splitted.Skip(1)));
    }

    protected bool IsNestedProperty(string PropertyName)
    {
        return PropertyName.Contains('.');
    }
}
