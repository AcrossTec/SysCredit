namespace SysCredit.Api.Attributes;

using FluentValidation;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ValidatorAttribute<TValidator> : Attribute where TValidator : IValidator
{
    public Type ValidatorType { get; } = typeof(TValidator);
}
