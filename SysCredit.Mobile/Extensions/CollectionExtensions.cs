namespace SysCredit.Mobile.Extensions;

using Sharpnado.CollectionView.ViewModels;

using System.Collections.Generic;

public static class CollectionExtensions
{
    public static ObservableRangeCollection<TSource> ToObservableRangeCollection<TSource>(this IEnumerable<TSource> Source)
    {
        return new ObservableRangeCollection<TSource>(Source);
    }

    public static async ValueTask<ObservableRangeCollection<TSource>> ToObservableRangeCollectionAsync<TSource>(this IAsyncEnumerable<TSource> Source)
    {
        var Collection = new ObservableRangeCollection<TSource>();

        await foreach (var Item in Source)
        {
            Collection.Add(Item);
        }

        return Collection;
    }
}
