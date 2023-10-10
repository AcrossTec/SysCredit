namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>
///     Extension methods for the <see cref="AttributeData"/> type.
/// </summary>
public static class AttributeDataExtensions
{
    /// <summary>
    ///     Checks whether a given <see cref="AttributeData"/> instance contains a specified named argument.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of argument to check.
    /// </typeparam>
    /// <param name="AttributeData">
    ///     The target <see cref="AttributeData"/> instance to check.
    /// </param>
    /// <param name="Name">
    ///     The name of the argument to check.
    /// </param>
    /// <param name="Value">
    ///     The expected value for the target named argument.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="AttributeData"/> contains an argument named <paramref name="Name"/> with the expected value.
    /// </returns>
    public static bool HasNamedArgument<T>(this AttributeData AttributeData, string Name, T? Value)
    {
        foreach (KeyValuePair<string, TypedConstant> Properties in AttributeData.NamedArguments)
        {
            if (Properties.Key == Name)
            {
                return Properties.Value.Value is T ArgumentValue
                    && EqualityComparer<T?>.Default.Equals(ArgumentValue, Value);
            }
        }

        return false;
    }

    /// <summary>
    ///     Tries to get the location of the input <see cref="AttributeData"/> instance.
    /// </summary>
    /// <param name="AttributeData">
    ///     The input <see cref="AttributeData"/> instance to get the location for.
    /// </param>
    /// <returns>
    ///     The resulting location for <paramref name="AttributeData"/>, if a syntax reference is available.
    /// </returns>
    public static Location? GetLocation(this AttributeData AttributeData)
    {
        if (AttributeData.ApplicationSyntaxReference is { } SyntaxReference)
        {
            return SyntaxReference.SyntaxTree.GetLocation(SyntaxReference.Span);
        }

        return null;
    }

    /// <summary>
    ///     Gets a given named argument value from an <see cref="AttributeData"/> instance, or a fallback value.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of argument to check.
    /// </typeparam>
    /// <param name="AttributeData">
    ///     The target <see cref="AttributeData"/> instance to check.
    /// </param>
    /// <param name="Name">
    ///     The name of the argument to check.
    /// </param>
    /// <param name="Fallback">
    ///     The fallback value to use if the named argument is not present.
    /// </param>
    /// <returns>
    ///     The argument named <paramref name="Name"/>, or a fallback value.
    /// </returns>
    public static T? GetNamedArgument<T>(this AttributeData AttributeData, string Name, T? Fallback = default)
    {
        if (AttributeData.TryGetNamedArgument(Name, out T? value))
        {
            return value;
        }

        return Fallback;
    }

    /// <summary>
    ///     Tries to get a given named argument value from an <see cref="AttributeData"/> instance, if present.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of argument to check.
    /// </typeparam>
    /// <param name="AttributeData">
    ///     The target <see cref="AttributeData"/> instance to check.
    /// </param>
    /// <param name="Name">
    ///     The name of the argument to check.
    /// </param>
    /// <param name="Value">
    ///     The resulting argument value, if present.
    /// </param>
    /// <returns>
    ///     Whether or not <paramref name="AttributeData"/> contains an argument named <paramref name="Name"/> with a valid value.
    /// </returns>
    public static bool TryGetNamedArgument<T>(this AttributeData AttributeData, string Name, out T? Value)
    {
        foreach (KeyValuePair<string, TypedConstant> Properties in AttributeData.NamedArguments)
        {
            if (Properties.Key == Name)
            {
                Value = (T?)Properties.Value.Value;
                return true;
            }
        }

        Value = default;
        return false;
    }

    /// <summary>
    ///     Enumerates all items in a flattened sequence of constructor arguments for a given <see cref="AttributeData"/> instance.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of constructor arguments to retrieve.
    /// </typeparam>
    /// <param name="AttributeData">
    ///     The target <see cref="AttributeData"/> instance to get the arguments from.
    /// </param>
    /// <returns>
    ///     A sequence of all constructor arguments of the specified type from <paramref name="AttributeData"/>.
    /// </returns>
    public static IEnumerable<T?> GetConstructorArguments<T>(this AttributeData AttributeData) where T : class
    {
        static IEnumerable<T?> Enumerate(IEnumerable<TypedConstant> Constants)
        {
            foreach (TypedConstant Constant in Constants)
            {
                if (Constant.IsNull)
                {
                    yield return null;
                }

                if (Constant.Kind == TypedConstantKind.Primitive && Constant.Value is T Value)
                {
                    yield return Value;
                }
                else if (Constant.Kind == TypedConstantKind.Array)
                {
                    foreach (T? Item in Enumerate(Constant.Values))
                    {
                        yield return Item;
                    }
                }
            }
        }

        return Enumerate(AttributeData.ConstructorArguments);
    }
}
