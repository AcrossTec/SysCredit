namespace SysCredit.Mobile.Converters;

using CommunityToolkit.Maui.Converters;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class MessageIfCollectionIsEmptyConverter : MessageIfCollectionIsEmptyConverter<object>
{
}

public class MessageIfCollectionIsEmptyConverter<TSource> : BaseConverterOneWay<IEnumerable<TSource>, string>
{
    public override string ConvertFrom(IEnumerable<TSource> Source, CultureInfo? Culture = null)
    {
        if (Source.Any())
        {
            return DefaultConvertReturnValue.Replace("{Count}", Source.Count().ToString());
        }

        return MessageIfCollectionIsEmpty;
    }

    public override string DefaultConvertReturnValue { get; set; } = string.Empty;

    public string MessageIfCollectionIsEmpty { get; set; } = string.Empty;
}
