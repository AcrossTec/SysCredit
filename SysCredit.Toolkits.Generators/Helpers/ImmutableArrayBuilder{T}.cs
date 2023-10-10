namespace SysCredit.Toolkits.Generators.Helpers;

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

/// <summary>
///     A helper type to build sequences of values with pooled buffers.
/// </summary>
/// <typeparam name="T">
///     The type of items to create sequences for.
/// </typeparam>
public ref struct ImmutableArrayBuilder<T>
{
    /// <summary>
    ///     The rented <see cref="Writer"/> instance to use.
    /// </summary>
    private Writer? _Writer;

    /// <summary>
    ///     Creates a <see cref="ImmutableArrayBuilder{T}"/> value with a pooled underlying data writer.
    /// </summary>
    /// <returns>
    ///     A <see cref="ImmutableArrayBuilder{T}"/> instance to write data to.
    /// </returns>
    public static ImmutableArrayBuilder<T> Rent()
    {
        return new(new Writer());
    }

    /// <summary>
    ///     Creates a new <see cref="ImmutableArrayBuilder{T}"/> object with the specified parameters.
    /// </summary>
    /// <param name="Writer">
    ///     The target data writer to use.
    /// </param>
    private ImmutableArrayBuilder(Writer Writer)
    {
        this._Writer = Writer;
    }

    /// <inheritdoc cref="ImmutableArray{T}.Builder.Count"/>
    public readonly int Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this._Writer!.Count;
    }

    /// <summary>
    ///     Gets the data written to the underlying buffer so far, as a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <remarks>
    ///     [UnscopedRef]
    /// </remarks>
    public readonly ReadOnlySpan<T> WrittenSpan
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this._Writer!.WrittenSpan;
    }

    /// <inheritdoc cref="ImmutableArray{T}.Builder.Add(T)"/>
    public readonly void Add(T Item)
    {
        this._Writer!.Add(Item);
    }

    /// <summary>
    ///     Adds the specified items to the end of the array.
    /// </summary>
    /// <param name="Items">
    ///     The items to add at the end of the array.
    /// </param>
    public readonly void AddRange(scoped ReadOnlySpan<T> Items)
    {
        this._Writer!.AddRange(Items);
    }

    /// <inheritdoc cref="ImmutableArray{T}.Builder.ToImmutable"/>
    public readonly ImmutableArray<T> ToImmutable()
    {
        T[] Array = this._Writer!.WrittenSpan.ToArray();
        return Unsafe.As<T[], ImmutableArray<T>>(ref Array);
    }

    /// <inheritdoc cref="ImmutableArray{T}.Builder.ToArray"/>
    public readonly T[] ToArray()
    {
        return this._Writer!.WrittenSpan.ToArray();
    }

    /// <summary>
    ///     Gets an <see cref="IEnumerable{T}"/> instance for the current builder.
    /// </summary>
    /// <returns>
    ///     An <see cref="IEnumerable{T}"/> instance for the current builder.
    /// </returns>
    /// <remarks>
    ///     The builder should not be mutated while an enumerator is in use.
    /// </remarks>
    public readonly IEnumerable<T> AsEnumerable()
    {
        return this._Writer!;
    }

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        return this._Writer!.WrittenSpan.ToString();
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
        Writer? Writer = this._Writer;
        this._Writer = null;
        Writer?.Dispose();
    }

    /// <summary>
    ///     A class handling the actual buffer writing.
    /// </summary>
    private sealed class Writer : ICollection<T>, IDisposable
    {
        /// <summary>
        ///     The underlying <typeparamref name="T"/> array.
        /// </summary>
        private T?[]? Array;

        /// <summary>
        /// The starting offset within <see cref="Array"/>.
        /// </summary>
        private int Index;

        /// <summary>
        ///     Creates a new <see cref="Writer"/> instance with the specified parameters.
        /// </summary>
        public Writer()
        {
            this.Array = ArrayPool<T?>.Shared.Rent(typeof(T) == typeof(char) ? 1024 : 8);
            this.Index = 0;
        }

        /// <inheritdoc cref="ImmutableArrayBuilder{T}.Count"/>
        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.Index;
        }

        /// <inheritdoc cref="ImmutableArrayBuilder{T}.WrittenSpan"/>
        public ReadOnlySpan<T> WrittenSpan
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new(this.Array!, 0, this.Index);
        }

        /// <inheritdoc/>
        bool ICollection<T>.IsReadOnly => true;

        /// <inheritdoc cref="ImmutableArrayBuilder{T}.Add"/>
        public void Add(T Value)
        {
            EnsureCapacity(1);
            this.Array![this.Index++] = Value;
        }

        /// <inheritdoc cref="ImmutableArrayBuilder{T}.AddRange"/>
        public void AddRange(ReadOnlySpan<T> Items)
        {
            EnsureCapacity(Items.Length);
            Items.CopyTo(this.Array.AsSpan(this.Index)!);
            this.Index += Items.Length;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            T?[]? Array = this.Array;
            this.Array = null;

            if (Array is not null)
            {
                ArrayPool<T?>.Shared.Return(Array, clearArray: typeof(T) != typeof(char));
            }
        }

        /// <inheritdoc/>
        void ICollection<T>.Clear()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        bool ICollection<T>.Contains(T Item)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        void ICollection<T>.CopyTo(T[] Array, int ArrayIndex)
        {
            System.Array.Copy(this.Array!, 0, Array, ArrayIndex, this.Index);
        }

        /// <inheritdoc/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            T?[] Array = this.Array!;
            int Length = this.Index;

            for (int Index = 0; Index < Length; ++Index)
            {
                yield return Array[Index]!;
            }
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        /// <inheritdoc/>
        bool ICollection<T>.Remove(T Item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Ensures that <see cref="Array"/> has enough free space to contain a given number of new items.
        /// </summary>
        /// <param name="RequestedSize">
        ///     The minimum number of items to ensure space for in <see cref="Array"/>.
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureCapacity(int RequestedSize)
        {
            if (RequestedSize > this.Array!.Length - this.Index)
            {
                ResizeBuffer(RequestedSize);
            }
        }

        /// <summary>
        ///     Resizes <see cref="Array"/> to ensure it can fit the specified number of new items.
        /// </summary>
        /// <param name="SizeHint">
        ///     The minimum number of items to ensure space for in <see cref="Array"/>.
        /// </param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ResizeBuffer(int SizeHint)
        {
            int MinimumSize = this.Index + SizeHint;

            T?[] OldArray = this.Array!;
            T?[] NewArray = ArrayPool<T?>.Shared.Rent(MinimumSize);

            System.Array.Copy(OldArray, NewArray, this.Index);

            this.Array = NewArray;
            ArrayPool<T?>.Shared.Return(OldArray, clearArray: typeof(T) != typeof(char));
        }
    }
}
