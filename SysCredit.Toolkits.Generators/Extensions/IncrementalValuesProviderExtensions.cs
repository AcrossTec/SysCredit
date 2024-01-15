namespace SysCredit.Toolkits.Generators.Extensions;

using Microsoft.CodeAnalysis;

using SysCredit.Toolkits.Generators.Helpers;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

/// <summary>
///     Extension methods for <see cref="IncrementalValuesProvider{TValues}"/>.
/// </summary>
public static class IncrementalValuesProviderExtensions
{
    /// <summary>
    ///     Groups items in a given <see cref="IncrementalValuesProvider{TValue}"/> sequence by a specified key.
    /// </summary>
    /// <typeparam name="TLeft">
    ///     The type of left items in each tuple.
    /// </typeparam>
    /// <typeparam name="TRight">
    ///     The type of right items in each tuple.
    /// </typeparam>
    /// <typeparam name="TKey">
    ///     The type of resulting key elements.
    /// </typeparam>
    /// <typeparam name="TElement">
    ///     The type of resulting projected elements.
    /// </typeparam>
    /// <param name="Source">
    ///     The input <see cref="IncrementalValuesProvider{TValues}"/> instance.
    /// </param>
    /// <param name="KeySelector">
    ///     The key selection <see cref="Func{T, TResult}"/>.
    /// </param>
    /// <param name="ElementSelector">
    ///     The element selection <see cref="Func{T, TResult}"/>.
    /// </param>
    /// <returns>
    ///     An <see cref="IncrementalValuesProvider{TValues}"/> with the grouped results.
    /// </returns>
    public static IncrementalValuesProvider<(TKey Key, EquatableArray<TElement> Right)> GroupBy<TLeft, TRight, TKey, TElement>(
        this IncrementalValuesProvider<(TLeft Left, TRight Right)> Source,
        Func<(TLeft Left, TRight Right), TKey> KeySelector,
        Func<(TLeft Left, TRight Right), TElement> ElementSelector)
        where TLeft : IEquatable<TLeft>
        where TRight : IEquatable<TRight>
        where TKey : IEquatable<TKey>
        where TElement : IEquatable<TElement>
    {
        return Source.Collect().SelectMany((Item, Token) =>
        {
            Dictionary<TKey, ImmutableArray<TElement>.Builder> Map = new();

            foreach ((TLeft, TRight) Pair in Item)
            {
                TKey key = KeySelector(Pair);
                TElement Element = ElementSelector(Pair);

                if (!Map.TryGetValue(key, out ImmutableArray<TElement>.Builder Builder))
                {
                    Builder = ImmutableArray.CreateBuilder<TElement>();
                    Map.Add(key, Builder);
                }

                Builder.Add(Element);
            }

            Token.ThrowIfCancellationRequested();

            ImmutableArray<(TKey Key, EquatableArray<TElement> Elements)>.Builder Result =
                ImmutableArray.CreateBuilder<(TKey, EquatableArray<TElement>)>();

            foreach (KeyValuePair<TKey, ImmutableArray<TElement>.Builder> Entry in Map)
            {
                Result.Add((Entry.Key, Entry.Value.ToImmutable()));
            }

            return Result;
        });
    }

    /// <summary>
    ///     Regresa un <see cref="IncrementalValuesProvider{TValues}"/> con valores únicos.
    /// </summary>
    /// <typeparam name="TSource">
    ///     Tipo del proveedor de datos.
    /// </typeparam>
    /// <param name="Source">
    ///     Proveedor de datos.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="IncrementalValuesProvider{TValues}"/> con valores únicos.
    /// </returns>
    public static IncrementalValuesProvider<TSource> Distinct<TSource>(this IncrementalValuesProvider<TSource> Source)
    {
        return Source.Distinct(EqualityComparer<TSource>.Default);
    }

    /// <summary>
    ///     Regresa un <see cref="IncrementalValuesProvider{TValues}"/> con valores únicos.
    /// </summary>
    /// <typeparam name="TSource">
    ///     Tipo del proveedor de datos.
    /// </typeparam>
    /// <param name="Source">
    ///     Proveedor de datos.
    /// </param>
    /// <param name="Comparer">
    ///     Comparador de los datos del proveedor.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="IncrementalValuesProvider{TValues}"/> con valores únicos.
    /// </returns>
    public static IncrementalValuesProvider<TSource> Distinct<TSource>(this IncrementalValuesProvider<TSource> Source, IEqualityComparer<TSource> Comparer)
    {
        return Source.Collect().SelectMany((Items, Token) =>
        {
            ImmutableHashSet<TSource>.Builder Builder = ImmutableHashSet.CreateBuilder(Comparer);

            foreach (var Value in Items)
            {
                Builder.Add(Value);
            }

            return Builder;
        });
    }
}
