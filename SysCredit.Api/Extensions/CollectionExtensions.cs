namespace SysCredit.Api.Extensions;

using SysCredit.Api.Requests;

using System.Collections;
using System.Data;
using System.Reflection;

/// <summary>
/// 
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Sources"></param>
    /// <returns></returns>
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
