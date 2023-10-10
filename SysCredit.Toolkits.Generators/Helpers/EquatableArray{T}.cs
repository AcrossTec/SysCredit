namespace SysCredit.Toolkits.Generators.Helpers;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

/// <summary>
///     Extensions for <see cref="EquatableArray{T}"/>.
/// </summary>
public static class EquatableArray
{
    /// <summary>
    ///     Creates an <see cref="EquatableArray{T}"/> instance from a given <see cref="ImmutableArray{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of items in the input array.
    /// </typeparam>
    /// <param name="Array">
    ///     The input <see cref="ImmutableArray{T}"/> instance.
    /// </param>
    /// <returns>
    ///     An <see cref="EquatableArray{T}"/> instance from a given <see cref="ImmutableArray{T}"/>.
    /// </returns>
    public static EquatableArray<T> AsEquatableArray<T>(this ImmutableArray<T> Array) where T : IEquatable<T>
    {
        return new(Array);
    }
}

/// <summary>
///     An immutable, equatable array. This is equivalent to <see cref="ImmutableArray{T}"/> but with value equality support.
/// </summary>
/// <typeparam name="T">
///     The type of values in the array.
/// </typeparam>
/// <remarks>
///     Creates a new <see cref="EquatableArray{T}"/> instance.
/// </remarks>
/// <param name="Array">
///     The input <see cref="ImmutableArray{T}"/> to wrap.
/// </param>
public readonly struct EquatableArray<T>(ImmutableArray<T> Array) : IEquatable<EquatableArray<T>>, IEnumerable<T> where T : IEquatable<T>
{
    /// <summary>
    ///     The underlying <typeparamref name="T"/> array.
    /// </summary>
    private readonly T[]? Array = Unsafe.As<ImmutableArray<T>, T[]?>(ref Array);

    /// <summary>
    ///     Gets a reference to an item at a specified position within the array.
    /// </summary>
    /// <param name="Index">
    ///     The index of the item to retrieve a reference to.
    /// </param>
    /// <returns>
    ///     A reference to an item at a specified position within the array.
    /// </returns>
    public ref readonly T this[int Index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref AsImmutableArray().ItemRef(Index);
    }

    /// <summary>
    ///     Gets a value indicating whether the current array is empty.
    /// </summary>
    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => AsImmutableArray().IsEmpty;
    }

    /// <inheritdoc/>
    public bool Equals(EquatableArray<T> Array)
    {
        return AsSpan().SequenceEqual(Array.AsSpan());
    }

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? @object)
    {
        return @object is EquatableArray<T> Array && Equals(this, Array);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        if (this.Array is not T[] Array)
        {
            return 0;
        }

        HashCode HashCode = default;

        foreach (T Item in Array)
        {
            HashCode.Add(Item);
        }

        return HashCode.ToHashCode();
    }

    /// <summary>
    ///     Gets an <see cref="ImmutableArray{T}"/> instance from the current <see cref="EquatableArray{T}"/>.
    /// </summary>
    /// <returns>
    ///     The <see cref="ImmutableArray{T}"/> from the current <see cref="EquatableArray{T}"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ImmutableArray<T> AsImmutableArray()
    {
        return Unsafe.As<T[]?, ImmutableArray<T>>(ref Unsafe.AsRef(in this.Array));
    }

    /// <summary>
    ///     Creates an <see cref="EquatableArray{T}"/> instance from a given <see cref="ImmutableArray{T}"/>.
    /// </summary>
    /// <param name="Array">
    ///     The input <see cref="ImmutableArray{T}"/> instance.
    /// </param>
    /// <returns>
    ///     An <see cref="EquatableArray{T}"/> instance from a given <see cref="ImmutableArray{T}"/>.
    /// </returns>
    public static EquatableArray<T> FromImmutableArray(ImmutableArray<T> Array)
    {
        return new(Array);
    }

    /// <summary>
    ///     Returns a <see cref="ReadOnlySpan{T}"/> wrapping the current items.
    /// </summary>
    /// <returns>
    ///     A <see cref="ReadOnlySpan{T}"/> wrapping the current items.
    /// </returns>
    public ReadOnlySpan<T> AsSpan()
    {
        return AsImmutableArray().AsSpan();
    }

    /// <summary>
    ///     Copies the contents of this <see cref="EquatableArray{T}"/> instance to a mutable array.
    /// </summary>
    /// <returns>
    ///     The newly instantiated array.
    /// </returns>
    public T[] ToArray()
    {
        return AsImmutableArray().ToArray();
    }

    /// <summary>
    ///     Gets an <see cref="ImmutableArray{T}.Enumerator"/> value to traverse items in the current array.
    /// </summary>
    /// <returns>
    ///     An <see cref="ImmutableArray{T}.Enumerator"/> value to traverse items in the current array.
    /// </returns>
    public ImmutableArray<T>.Enumerator GetEnumerator()
    {
        return AsImmutableArray().GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return ((IEnumerable<T>)AsImmutableArray()).GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)AsImmutableArray()).GetEnumerator();
    }

    /// <summary>
    ///     Implicitly converts an <see cref="ImmutableArray{T}"/> to <see cref="EquatableArray{T}"/>.
    /// </summary>
    /// <returns>
    ///     An <see cref="EquatableArray{T}"/> instance from a given <see cref="ImmutableArray{T}"/>.
    /// </returns>
    public static implicit operator EquatableArray<T>(ImmutableArray<T> Array)
    {
        return FromImmutableArray(Array);
    }

    /// <summary>
    ///     Implicitly converts an <see cref="EquatableArray{T}"/> to <see cref="ImmutableArray{T}"/>.
    /// </summary>
    /// <returns>
    ///     An <see cref="ImmutableArray{T}"/> instance from a given <see cref="EquatableArray{T}"/>.
    /// </returns>
    public static implicit operator ImmutableArray<T>(EquatableArray<T> Array)
    {
        return Array.AsImmutableArray();
    }

    /// <summary>
    ///     Checks whether two <see cref="EquatableArray{T}"/> values are the same.
    /// </summary>
    /// <param name="Left">
    ///     The first <see cref="EquatableArray{T}"/> value.
    /// </param>
    /// <param name="Right">
    ///     The second <see cref="EquatableArray{T}"/> value.
    /// </param>
    /// <returns>
    ///     Whether <paramref name="Left"/> and <paramref name="Right"/> are equal.
    /// </returns>
    public static bool operator ==(EquatableArray<T> Left, EquatableArray<T> Right)
    {
        return Left.Equals(Right);
    }

    /// <summary>
    ///     Checks whether two <see cref="EquatableArray{T}"/> values are not the same.
    /// </summary>
    /// <param name="Left">
    ///     The first <see cref="EquatableArray{T}"/> value.
    /// </param>
    /// <param name="Right">
    ///     The second <see cref="EquatableArray{T}"/> value.
    /// </param>
    /// <returns>
    ///     Whether <paramref name="Left"/> and <paramref name="Right"/> are not equal.
    /// </returns>
    public static bool operator !=(EquatableArray<T> Left, EquatableArray<T> Right)
    {
        return !Left.Equals(Right);
    }
}
