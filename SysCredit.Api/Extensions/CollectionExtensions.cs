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
    /// <param name="Sources">
    ///     Colección que se va ha convertir en un <see cref="DataTable" />.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="DataTable" /> como el resultado de convertir <paramref name="Sources" />.
    /// </returns>
    public static DataTable ToDataTable(this IEnumerable<IRequest> Sources)
    {
        DataTable Table = new DataTable();

        Type ViewModelType = Sources.GetType().GetInterface($"{nameof(IEnumerable)}`1")!.GenericTypeArguments.First();
        PropertyInfo[] Properties = ViewModelType.GetProperties();

        foreach (PropertyInfo Property in Properties)
        {
            var ColumnType = Property.PropertyType.GetUnderlyingType();
            Table.Columns.Add(Property.Name, ColumnType);
        }

        foreach (var ViewModel in Sources)
        {
            Table.Rows.Add(Properties.Select(Property => Property.GetValue(ViewModel)).ToArray());
        }

        return Table;
    }
}
