namespace SysCredit.Api.Attributes;

using FluentValidation;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TValidator"></typeparam>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ValidatorAttribute<TValidator> : Attribute where TValidator : IValidator
{
    /// <summary>
    /// 
    /// </summary>
    public readonly Type ValidatorType = typeof(TValidator);
}
