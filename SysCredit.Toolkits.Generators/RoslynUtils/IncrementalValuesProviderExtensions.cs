using System.Collections.Immutable;

using Microsoft.CodeAnalysis;

/// <summary>
/// 
/// </summary>
public static class IncrementalValuesProviderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="Source"></param>
    /// <param name="SourceToElementTransform"></param>
    /// <param name="Comparer"></param>
    /// <returns></returns>
    public static IncrementalValuesProvider<(TSource Source, int Index, ImmutableArray<TElement> Elements)> GroupWith<TSource, TElement>(this IncrementalValuesProvider<TSource> Source, Func<TSource, TElement> SourceToElementTransform, IEqualityComparer<TSource> Comparer)
    {
        return Source.Collect().SelectMany((Values, _) =>
        {
            Dictionary<TSource, ImmutableArray<TElement>.Builder> Map = new(Comparer);

            foreach (var Value in Values)
            {
                if (!Map.TryGetValue(Value, out ImmutableArray<TElement>.Builder Builder))
                {
                    Builder = ImmutableArray.CreateBuilder<TElement>();
                    Map.Add(Value, Builder);
                }

                Builder.Add(SourceToElementTransform(Value));
            }

            ImmutableArray<(TSource Key, int Index, ImmutableArray<TElement> Elements)>.Builder Result =
                ImmutableArray.CreateBuilder<(TSource, int, ImmutableArray<TElement>)>();

            var Index = 0;
            foreach (var Entry in Map)
            {
                Result.Add((Entry.Key, Index, Entry.Value.ToImmutable()));
                Index++;
            }

            return Result;
        });
    }
}
