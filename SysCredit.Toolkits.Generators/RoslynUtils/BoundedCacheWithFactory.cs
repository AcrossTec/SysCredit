namespace Microsoft.AspNetCore.Analyzers.Infrastructure;

using System;
using System.Collections.Generic;

// This type is copied from https://github.com/dotnet/roslyn-analyzers/blob/9b58ec3ad33353d1a523cda8c4be38eaefc80ad8/src/Utilities/Compiler/BoundedCacheWithFactory.cs

/// <summary>
///     Provides bounded cache for analyzers.
///     Acts as a good alternative to <see cref="System.Runtime.CompilerServices.ConditionalWeakTable{TKey, TValue}"/>
///     when the cached value has a cyclic reference to the key preventing early garbage collection of entries.
/// </summary>
public class BoundedCacheWithFactory<TKey, TValue> where TKey : class
{
    // Bounded weak reference cache.
    // Size 5 is an arbitrarily chosen bound, which can be tuned in future as required.
    private readonly List<WeakReference<Entry?>> _WeakReferencedEntries =
    [
        new WeakReference<Entry?>(null),
        new WeakReference<Entry?>(null),
        new WeakReference<Entry?>(null),
        new WeakReference<Entry?>(null),
        new WeakReference<Entry?>(null),
    ];

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="ValueFactory"></param>
    /// <returns></returns>
    public TValue GetOrCreateValue(TKey Key, Func<TKey, TValue> ValueFactory)
    {
        lock (_WeakReferencedEntries)
        {
            var IndexToSetTarget = -1;
            for (var I = 0; I < _WeakReferencedEntries.Count; ++I)
            {
                var WeakReferencedEntry = _WeakReferencedEntries[I];

                if (!WeakReferencedEntry.TryGetTarget(out var CachedEntry) || CachedEntry == null)
                {
                    if (IndexToSetTarget == -1)
                    {
                        IndexToSetTarget = I;
                    }

                    continue;
                }

                if (Equals(CachedEntry.Key, Key))
                {
                    // Move the cache hit item to the end of the list
                    // so it would be least likely to be evicted on next cache miss.
                    _WeakReferencedEntries.RemoveAt(I);
                    _WeakReferencedEntries.Add(WeakReferencedEntry);
                    return CachedEntry.Value;
                }
            }

            if (IndexToSetTarget == -1)
            {
                IndexToSetTarget = 0;
            }

            var NewEntry = new Entry(Key, ValueFactory(Key));
            _WeakReferencedEntries[IndexToSetTarget].SetTarget(NewEntry);
            return NewEntry.Value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private sealed class Entry(TKey key, TValue value)
    {
        public TKey Key { get; } = key;

        public TValue Value { get; } = value;
    }
}
