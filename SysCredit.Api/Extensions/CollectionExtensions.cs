namespace SysCredit.Api.Extensions;

using SysCredit.Api.ViewModels;

using System.Collections;
using System.Data;
using System.Reflection;

public static class CollectionExtensions
{
    public static DataTable ToDataTable(this IEnumerable<IViewModel> ViewModels)
    {
        DataTable Table = new DataTable();

        Type ViewModelType = ViewModels.GetType().GetInterface($"{nameof(IEnumerable)}`1")!.GenericTypeArguments.First();
        PropertyInfo[] Properties = ViewModelType.GetProperties();

        foreach (PropertyInfo Property in Properties)
        {
            var ColumnType = Property.PropertyType.GetUnderlyingType();
            Table.Columns.Add(Property.Name, ColumnType);
        }

        foreach (var ViewModel in ViewModels)
        {
            Table.Rows.Add(Properties.Select(Property => Property.GetValue(ViewModel)).ToArray());
        }

        return Table;
    }
}
