namespace Microsoft.AspNetCore.Shared;

using Microsoft.CodeAnalysis;

/// <summary>
/// 
/// </summary>
public static class MvcFacts
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="ControllerAttribute"></param>
    /// <param name="NonControllerAttribute"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool IsController(INamedTypeSymbol Type, INamedTypeSymbol ControllerAttribute, INamedTypeSymbol NonControllerAttribute)
    {
        Type = Type ?? throw new ArgumentNullException(nameof(Type));
        ControllerAttribute = ControllerAttribute ?? throw new ArgumentNullException(nameof(ControllerAttribute));
        NonControllerAttribute = NonControllerAttribute ?? throw new ArgumentNullException(nameof(NonControllerAttribute));

        if (Type.TypeKind != TypeKind.Class)
        {
            return false;
        }

        if (Type.IsAbstract)
        {
            return false;
        }

        // We only consider public top-level classes as controllers.
        if (Type.DeclaredAccessibility != Accessibility.Public)
        {
            return false;
        }

        if (Type.ContainingType != null)
        {
            return false;
        }

        if (Type.IsGenericType || Type.IsUnboundGenericType)
        {
            return false;
        }

        if (Type.HasAttribute(NonControllerAttribute, Inherit: true))
        {
            return false;
        }

        if (!Type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) && !Type.HasAttribute(ControllerAttribute, Inherit: true))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Method"></param>
    /// <param name="NonActionAttribute"></param>
    /// <param name="DisposableDispose"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool IsControllerAction(IMethodSymbol Method, INamedTypeSymbol NonActionAttribute, IMethodSymbol DisposableDispose)
    {
        Method = Method ?? throw new ArgumentNullException(nameof(Method));
        NonActionAttribute = NonActionAttribute ?? throw new ArgumentNullException(nameof(NonActionAttribute));

        if (Method.MethodKind != MethodKind.Ordinary)
        {
            return false;
        }

        if (Method.HasAttribute(NonActionAttribute, Inherit: true))
        {
            return false;
        }

        // Overridden methods from Object class, e.g. Equals(Object), GetHashCode(), etc., are not valid.
        if (GetDeclaringType(Method).SpecialType == SpecialType.System_Object)
        {
            return false;
        }

        if (IsIDisposableDispose(Method, DisposableDispose))
        {
            return false;
        }

        if (Method.IsStatic)
        {
            return false;
        }

        if (Method.IsAbstract)
        {
            return false;
        }

        if (Method.IsGenericMethod)
        {
            return false;
        }

        return Method.DeclaredAccessibility == Accessibility.Public;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Method"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static INamedTypeSymbol GetDeclaringType(IMethodSymbol Method)
    {
        while (Method.IsOverride)
        {
            if (Method.OverriddenMethod == null)
            {
                throw new InvalidOperationException($"{nameof(Method.OverriddenMethod)} cannot be null.");
            }

            Method = Method.OverriddenMethod;
        }

        return Method.ContainingType;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Method"></param>
    /// <param name="DisposableDispose"></param>
    /// <returns></returns>
    private static bool IsIDisposableDispose(IMethodSymbol Method, IMethodSymbol DisposableDispose)
    {
        if (Method.Name != DisposableDispose.Name)
        {
            return false;
        }

        if (!Method.ReturnsVoid)
        {
            return false;
        }

        if (Method.Parameters.Length != DisposableDispose.Parameters.Length)
        {
            return false;
        }

        // Explicit implementation
        for (var I = 0; I < Method.ExplicitInterfaceImplementations.Length; ++I)
        {
            if (Method.ExplicitInterfaceImplementations[I].ContainingType.SpecialType == SpecialType.System_IDisposable)
            {
                return true;
            }
        }

        var ImplementedMethod = Method.ContainingType.FindImplementationForInterfaceMember(DisposableDispose);
        return SymbolEqualityComparer.Default.Equals(ImplementedMethod, Method);
    }
}
