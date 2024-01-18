namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;
using System.Linq;

/// <summary>
///     Métodos de utilería para las operaciones con la Semantica de Sintaxis del Arbol de Sintaxis de C#
/// </summary>
public static class Helpers
{
    /// <summary>
    ///     Obtiene el nombre del archivo que se está auto-generando.
    /// </summary>
    /// <param name="Name">
    ///     Nombre del archivo al que se le agregará su sufijo de auto-generado.
    /// </param>
    /// <returns></returns>
    public static string FileName(string Name)
        => $"{Name}{Constants.GeneratedFileExtension}";

    /// <summary>
    ///     Realiza un Cast de un objeto a otro compatible.
    /// </summary>
    /// <typeparam name="TObject">
    ///     Tipo del objeto al que se convertirá <paramref name="object"/>.
    /// </typeparam>
    /// <param name="object">
    ///     Objeto que se convertirá al tipo <typeparamref name="TObject"/>.
    /// </param>
    /// <returns>
    ///     Regresa el objeto convertido al tipo indicado.
    /// </returns>
    public static TObject? As<TObject>(this object? @object)
        => (TObject?)@object;

    /// <summary>
    ///     Verifica si un objeto no es <see langword="null"/>.
    /// </summary>
    /// <typeparam name="TSource">
    ///     Tipo del objeto que se verificará si no es <see langword="null"/>.
    /// </typeparam>
    /// <param name="Source">
    ///     Objeto que se verificará si no es <see langword="null"/>.
    /// </param>
    /// <returns>
    ///     Regresa <see langword="true"/> si <paramref name="Source"/> no es <see langword="null"/> en caso contrario <see langword="false"/>.
    /// </returns>
    public static bool WhereNotNull<TSource>(TSource? Source)
        => Source is not null;

    /// <summary>
    ///     Obtiene el objeto semántico <see cref="INamedTypeSymbol"/> asociado al atributo <see cref="Constants.ErrorCodePrefixAttribute"/>.
    /// </summary>
    /// <param name="Compilation">
    ///     Objeto que contiene toda la información del arbol de sintaxis C#.
    /// </param>
    /// <returns>
    ///     Regresa el objeto semántico <see cref="INamedTypeSymbol"/> asociado al atributo <see cref="Constants.ErrorCodePrefixAttribute"/>.
    /// </returns>
    public static INamedTypeSymbol GetErrorCodePrefixAttributeMetadataName(this Compilation Compilation)
        => Compilation.GetTypeByMetadataName(Constants.ErrorCodePrefixAttribute)!;

    /// <summary>
    ///     Obtiene el objeto semántico <see cref="INamedTypeSymbol"/> asociado al atributo <see cref="Constants.ErrorCodeRangeAttribute"/>.
    /// </summary>
    /// <param name="Compilation">
    ///     Objeto que contiene toda la información del arbol de sintaxis C#.
    /// </param>
    /// <returns>
    ///     Regresa el objeto semántico <see cref="INamedTypeSymbol"/> asociado al atributo <see cref="Constants.ErrorCodeRangeAttribute"/>.
    /// </returns>
    public static INamedTypeSymbol GetErrorCodeRangeAttributeMetadataName(this Compilation Compilation)
        => Compilation.GetTypeByMetadataName(Constants.ErrorCodeRangeAttribute)!;

    /// <summary>
    ///     Obtiene el objeto semántico <see cref="INamedTypeSymbol"/> asociado al atributo <see cref="Constants.ErrorCategoryAttribute"/>.
    /// </summary>
    /// <param name="Compilation">
    ///     Objeto que contiene toda la información del arbol de sintaxis C#.
    /// </param>
    /// <returns>
    ///     Regresa el objeto semántico <see cref="INamedTypeSymbol"/> asociado al atributo <see cref="Constants.ErrorCategoryAttribute"/>.
    /// </returns>
    public static INamedTypeSymbol GetErrorCategoryAttributeMetadataName(this Compilation Compilation)
        => Compilation.GetTypeByMetadataName(Constants.ErrorCategoryAttribute)!;

    /// <summary>
    ///     Obtiene el objeto semántico <see cref="INamedTypeSymbol"/> asociado al atributo <see cref="Constants.ManagerInterfaceAttribute"/>.
    /// </summary>
    /// <param name="Compilation">
    ///     Objeto que contiene toda la información del arbol de sintaxis C#.
    /// </param>
    /// <returns>
    ///     Regresa el objeto semántico <see cref="INamedTypeSymbol"/> asociado al atributo <see cref="Constants.ManagerInterfaceAttribute"/>.
    /// </returns>
    public static INamedTypeSymbol GetManagerInterfaceAttributeMetadataName(this Compilation Compilation)
        => Compilation.GetTypeByMetadataName(Constants.ManagerInterfaceAttribute)!;

    /// <summary>
    ///     Obtiene el objeto semántico <see cref="INamedTypeSymbol"/> asociado al atributo <see cref="Constants.ManagerModelAttribute"/>.
    /// </summary>
    /// <param name="Compilation">
    ///     Objeto que contiene toda la información del arbol de sintaxis C#.
    /// </param>
    /// <returns>
    ///     Regresa el objeto semántico <see cref="INamedTypeSymbol"/> asociado al atributo <see cref="Constants.ManagerModelAttribute"/>.
    /// </returns>
    public static INamedTypeSymbol GetManagerModelAttributeMetadataName(this Compilation Compilation)
        => Compilation.GetTypeByMetadataName(Constants.ManagerModelAttribute)!;

    /// <summary>
    ///     Obtiene el <see cref="AttributeData"/> de <paramref name="AttributeSymbol"/> si este existe en <paramref name="TypeSymbol"/>.
    /// </summary>
    /// <param name="TypeSymbol">
    ///     Objeto Semántico del que se obtendrá el <see cref="AttributeData"/>.
    /// </param>
    /// <param name="AttributeSymbol">
    ///     Atributo Semántico que se buscará en <paramref name="TypeSymbol"/>.
    /// </param>
    /// <returns>
    ///     Regresa el <see cref="AttributeData"/> del objeto semántico <paramref name="TypeSymbol"/> sólo si tiene el atributo semántico <paramref name="AttributeSymbol"/>.
    /// </returns>
    public static AttributeData? GetAttributeIfExists(this INamedTypeSymbol TypeSymbol, INamedTypeSymbol AttributeSymbol)
        => TypeSymbol.GetAttributes().GetAttributeIfExists(AttributeSymbol);

    /// <summary>
    ///     Busca el <paramref name="AttributeSymbol"/> desde un array de <see cref="AttributeData"/>.
    /// </summary>
    /// <param name="Source">
    ///     Array de <see cref="AttributeData"/>.
    /// </param>
    /// <param name="AttributeSymbol">
    ///     Atributo Semántico que se buscará.
    /// </param>
    /// <returns>
    ///     Regresa el <see cref="AttributeData"/> de <paramref name="AttributeSymbol"/> si este existe en <paramref name="Source"/>.
    /// </returns>
    public static AttributeData? GetAttributeIfExists(this IEnumerable<AttributeData> Source, INamedTypeSymbol AttributeSymbol)
        => Source.FirstOrDefault(Attribute => SymbolEqualityComparer.Default.Equals(AttributeSymbol, Attribute.AttributeClass));

    /// <summary>
    ///     Obtiene todos los atributos genéricos de <paramref name="MemberSyntax"/>.
    /// </summary>
    /// <param name="MemberSyntax">
    ///     Miembro que contiene los atributos genéricos.
    /// </param>
    /// <param name="Arity">
    ///     Número de argumentos genéricos que contiene cada atributo que se retornará.
    /// </param>
    /// <returns>
    ///     Regresa todos los atributos genéricos de <paramref name="MemberSyntax"/> que tengan el mismo <paramref name="Arity"/> de argumentos genéricos.
    /// </returns>
    public static IEnumerable<AttributeSyntax> GetGenericAttributes(this MemberDeclarationSyntax MemberSyntax, int Arity)
    {
        foreach (var Attribute in MemberSyntax.CollectAttributeList())
        {
            if (Attribute is { Name: GenericNameSyntax NameSyntax } && NameSyntax.Arity == Arity)
            {
                yield return Attribute;
            }
        }
    }

    /// <summary>
    ///     Obtiene el <see cref="GenericNameSyntax"/> de cada argumento genérico de <paramref name="MemberSyntax"/>.
    /// </summary>
    /// <param name="MemberSyntax">
    ///     Miembro que contiene los atributos genéricos.
    /// </param>
    /// <param name="Arity">
    ///     Número de argumentos genéricos que contiene cada atributo que se retornará.
    /// </param>
    /// <returns>
    ///     Regresa todos los argumentos genéricos de <paramref name="MemberSyntax"/> con la misma cantidad de argumentos genéricos de <paramref name="Arity"/>.
    /// </returns>
    public static IEnumerable<GenericNameSyntax> GetGenericNameSyntaxFromAttributes(this MemberDeclarationSyntax MemberSyntax, int Arity)
    {
        foreach (var Attribute in MemberSyntax.CollectAttributeList())
        {
            if (Attribute is { Name: GenericNameSyntax NameSyntax } && NameSyntax.Arity == Arity)
            {
                yield return NameSyntax;
            }
        }
    }

    /// <summary>
    ///     Regresa todos los atributos que posee <paramref name="MemberSyntax"/>.
    /// </summary>
    /// <param name="MemberSyntax">
    ///     Miembro del árbol de sintaxis del que se buscarán todos sus atributos.
    /// </param>
    /// <returns>
    ///     Regresa todos los atributos que posee <paramref name="MemberSyntax"/>.
    /// </returns>
    public static IEnumerable<AttributeSyntax> CollectAttributeList(this MemberDeclarationSyntax MemberSyntax)
        => MemberSyntax.AttributeLists.Collect();

    /// <summary>
    ///     Convierte todos los <see cref="AttributeListSyntax"/> en un Array de <see cref="AttributeSyntax"/>.
    /// </summary>
    /// <param name="AttributeLists">
    ///     Lista de <see cref="AttributeListSyntax"/>.
    /// </param>
    /// <returns>
    ///     Regresa una lista de <see cref="AttributeSyntax"/>.
    /// </returns>
    public static IEnumerable<AttributeSyntax> Collect(in this SyntaxList<AttributeListSyntax> AttributeLists)
        => AttributeLists.SelectMany(AttributeList => AttributeList.Attributes);

    /// <summary>
    ///     Verifica su en <paramref name="MemberSyntax"/> exise un atributo con el nombre en <paramref name="Name"/>.
    /// </summary>
    /// <param name="MemberSyntax">
    ///     Miembro donde se buscará el atributo con el nombre en <paramref name="Name"/>.
    /// </param>
    /// <param name="Name">
    ///     Nombre del atributo que se buscará en <paramref name="MemberSyntax"/>.
    /// </param>
    /// <returns>
    ///     Regresa <see langword="true"/> si el atributo con el nombre en <paramref name="Name"/> existe en <paramref name="MemberSyntax"/>.
    /// </returns>
    public static bool HasAttribute(this MemberDeclarationSyntax MemberSyntax, string Name)
        => MemberSyntax.AttributeLists.HasAttribute(Name);

    /// <summary>
    ///     Buscar el atributo <b>ManagerInterfaceAttribute</b> en <paramref name="MemberSyntax"/>.
    /// </summary>
    /// <param name="MemberSyntax">
    ///     Objeto de Sintaxis donde se buscará el atributo <b>ManagerInterfaceAttribute</b>.
    /// </param>
    /// <returns>
    ///     Regresa <see langword="true"/> si el atributo <b>ManagerInterfaceAttribute</b> existe en <paramref name="MemberSyntax"/>.
    /// </returns>
    public static bool HasManagerInterfaceAttribute(this MemberDeclarationSyntax MemberSyntax)
        => MemberSyntax.AttributeLists.HasManagerInterfaceAttribute();

    /// <summary>
    ///     Buscar el atributo <b>ManagerModelAttribute</b> en <paramref name="MemberSyntax"/>.
    /// </summary>
    /// <param name="MemberSyntax">
    ///     Objeto de Sintaxis donde se buscará el atributo <b>ManagerModelAttribute</b>.
    /// </param>
    /// <returns>
    ///     Regresa <see langword="true"/> si el atributo <b>ManagerModelAttribute</b> existe en <paramref name="MemberSyntax"/>.
    /// </returns>
    public static bool HasManagerModelAttribute(this MemberDeclarationSyntax MemberSyntax)
        => MemberSyntax.AttributeLists.HasManagerModelAttribute();

    /// <summary>
    ///     Verifica si es existe en atributo <b>ManagerInterfaceAttribute</b> en <paramref name="AttributeLists"/>.
    /// </summary>
    /// <param name="AttributeLists">
    ///     Lista de <see cref="AttributeListSyntax"/>.
    /// </param>
    /// <returns>
    ///     Regresa <see langword="true"/> si el atributo <b>ManagerInterfaceAttribute</b> existe en <paramref name="AttributeLists"/>.
    /// </returns>
    public static bool HasManagerInterfaceAttribute(in this SyntaxList<AttributeListSyntax> AttributeLists)
        => AttributeLists.HasGenericAttribute(Arity: 1, Name: "ManagerInterface") || AttributeLists.HasGenericAttribute(Arity: 1, Name: "ManagerInterfaceAttribute");

    /// <summary>
    ///     Verifica si es existe en atributo <b>ManagerModelAttribute</b> en <paramref name="AttributeLists"/>.
    /// </summary>
    /// <param name="AttributeLists">
    ///     Lista de <see cref="AttributeListSyntax"/>.
    /// </param>
    /// <returns>
    ///     Regresa <see langword="true"/> si el atributo <b>ManagerModelAttribute</b> existe en <paramref name="AttributeLists"/>.
    /// </returns>
    public static bool HasManagerModelAttribute(in this SyntaxList<AttributeListSyntax> AttributeLists)
        => AttributeLists.HasGenericAttribute(Arity: 1, Name: "ManagerModel") || AttributeLists.HasGenericAttribute(Arity: 1, Name: "ManagerModelAttribute");

    /// <summary>
    ///     Busca un atributo genérico con la misma cantidad de argumentos genéricos que <paramref name="Arity"/>
    ///     y con el mismo nombre contenido en <paramref name="Name"/> desde el array <paramref name="AttributeLists"/>.
    /// </summary>
    /// <param name="AttributeLists">
    ///     Array de <see cref="AttributeListSyntax"/>.
    /// </param>
    /// <param name="Arity">
    ///     Cantidad de argumentos genéricos que tiene el atributo que se buscará.
    /// </param>
    /// <param name="Name">
    ///     Nombre del atributo genérico que se buscará.
    /// </param>
    /// <returns>
    ///     Regresa <see langword="true"/> si el atributo genérico con el mismo nombre que <paramref name="Name"/> existe en <paramref name="AttributeLists"/>.
    /// </returns>
    public static bool HasGenericAttribute(in this SyntaxList<AttributeListSyntax> AttributeLists, int Arity, string Name)
    {
        foreach (var Attribute in AttributeLists.Collect())
        {
            if (Attribute is { Name: GenericNameSyntax NameSyntax } && NameSyntax.Arity == Arity && NameSyntax.Identifier.Text == Name)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Buscar el atributo con el nombre que contenga <paramref name="Name"/> en el array <paramref name="AttributeLists"/>.
    /// </summary>
    /// <param name="AttributeLists">
    ///     Array de <see cref="AttributeListSyntax"/> que se usará para buscar el atributo con el nombre contenido en <paramref name="Name"/>. 
    /// </param>
    /// <param name="Name">
    ///     Nombre del atributo que se buscará.
    /// </param>
    /// <returns>
    ///     Regresa <see langword="true"/> si el atributo con el nombre en <paramref name="Name"/> existe, en caso contrario <see langword="false"/>.
    /// </returns>
    public static bool HasAttribute(in this SyntaxList<AttributeListSyntax> AttributeLists, string Name)
    {
        foreach (var Attribute in AttributeLists.Collect())
        {
            if (Attribute is { Name: IdentifierNameSyntax { Arity: 0 } NameSyntax } && NameSyntax.Identifier.Text == Name)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Obtiene el espacio de nombre donde está declarado <paramref name="MemberSyntax"/>.
    /// </summary>
    /// <param name="MemberSyntax">
    ///     Objeto que se analizará y obtendrá su espacio de nombres.
    /// </param>
    /// <returns>
    ///     Regresa el espacio de nombres de <paramref name="MemberSyntax"/>.
    /// </returns>
    public static BaseNamespaceDeclarationSyntax? GetNamespaceDeclarationSyntax(this MemberDeclarationSyntax MemberSyntax)
    {
        var Current = MemberSyntax.Parent;

        while (Current is not null)
        {
            if (Current is BaseNamespaceDeclarationSyntax) break;
            Current = Current!.Parent;
        }

        return Current as BaseNamespaceDeclarationSyntax;
    }
}
