namespace SysCredit.Api.Extensions;

using SysCredit.Api.Requests;

using System.Collections;
using System.Data;
using System.Reflection;

/// <summary>
///     Métodos de utilería para operaciones sobre colecciones.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    ///     Convierte un array de <see cref="IRequest" /> en un <see cref="DataTable" />.
    /// </summary>
    /// <param name="Source">
    ///     Colección que se va ha convertir en un <see cref="DataTable" />.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="DataTable" /> como el resultado de convertir <paramref name="Source" />.
    /// </returns>
    public static DataTable ToDataTable(this IEnumerable<IRequest> Source)
    {
        DataTable Table = new DataTable();

        Type ViewModelType = Source.GetType().GetInterface($"{nameof(IEnumerable)}`1")!.GenericTypeArguments.First();
        PropertyInfo[] Properties = ViewModelType.GetProperties();

        foreach (PropertyInfo Property in Properties)
        {
            var ColumnType = Property.PropertyType.GetUnderlyingType();
            Table.Columns.Add(Property.Name, ColumnType);
        }

        foreach (var ViewModel in Source)
        {
            Table.Rows.Add(Properties.Select(Property => Property.GetValue(ViewModel)).ToArray());
        }

        return Table;
    }

    /// <summary>
    ///     Agrega un rango de elementos para <paramref name="Source" />.
    /// </summary>
    /// <typeparam name="TSource">
    ///     Tipo de la coleccion.
    /// </typeparam>
    /// <param name="Source">
    ///     Colección que se le agregarán los nuevos elementos.
    /// </param>
    /// <param name="Values">
    ///     Lista de valores que serán añadidos.
    /// </param>
    public static void AddRange<TSource>(this ICollection<TSource> Source, params TSource[] Values)
    {
        AddRange(Source, (IEnumerable<TSource>)Values);
    }

    /// <inheritdoc cref="AddRange{TSource}(ICollection{TSource}, TSource[])" />
    public static void AddRange<TSource>(this ICollection<TSource> Source, IEnumerable<TSource> Values)
    {
        foreach (TSource Value in Values)
        {
            Source.Add(Value);
        }
    }

    /// <summary>
    ///     Checks whether enumerable is null or empty.
    /// </summary>
    /// <typeparam name="TSource">
    ///     The type of the enumerable.
    /// </typeparam>
    /// <param name="Source">
    ///     The System.Collections.Generic.IEnumerable`1 to be checked.
    /// </param>
    /// <returns>
    ///     True if enumerable is null or empty, false otherwise.
    /// </returns>
    public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> Source)
    {
        return Source == null || !Source.Any();
    }

    /// <summary>
    ///     Regresa un <see cref="IEnumerable{T}" /> vacío si <paramref name="Source" /> es null sino un valor por defecto.
    /// </summary>
    /// <typeparam name="TSource">
    ///     Tipo de la colección.
    /// </typeparam>
    /// <param name="Source">
    ///     La colección ha chequear.
    /// </param>
    /// <param name="DefaultValue">
    ///     Valor por defecto en caso de que la coleccion sea nula o este vacía.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="Enumerable.Empty{TResult}" /> si <paramref name="Source" /> es null
    ///     sino un <see cref="Enumerable.DefaultIfEmpty{TSource}(IEnumerable{TSource}, TSource)" />.
    /// </returns>
    public static IEnumerable<TSource> DefaultIfNullOrEmpty<TSource>(this IEnumerable<TSource>? Source)
    {
        return Source ?? Enumerable.Empty<TSource>();
    }
}
