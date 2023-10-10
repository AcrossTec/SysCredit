#pragma warning disable CS0809

namespace System;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

/// <summary>
///     A polyfill type that mirrors some methods from <see cref="HashCode"/> on .NET 6.
/// </summary>
public struct HashCode
{
    private const uint Prime1 = 2654435761U;
    private const uint Prime2 = 2246822519U;
    private const uint Prime3 = 3266489917U;
    private const uint Prime4 = 668265263U;
    private const uint Prime5 = 374761393U;

    private static readonly uint Seed = GenerateGlobalSeed();

    private uint V1, V2, V3, V4;
    private uint Queue1, Queue2, Queue3;
    private uint Length;

    /// <summary>
    ///     Initializes the default seed.
    /// </summary>
    /// <returns>
    ///     A random seed.
    /// </returns>
    private static unsafe uint GenerateGlobalSeed()
    {
        byte[] Bytes = new byte[4];

        using (RandomNumberGenerator Generator = RandomNumberGenerator.Create())
        {
            Generator.GetBytes(Bytes);
        }

        return BitConverter.ToUInt32(Bytes, 0);
    }

    /// <summary>
    ///     Adds a single value to the current hash.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the value to add into the hash code.
    /// </typeparam>
    /// <param name="Value">
    ///     The value to add into the hash code.
    /// </param>
    public void Add<T>(T? Value)
    {
        Add(Value?.GetHashCode() ?? 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Initialize(out uint V1, out uint V2, out uint V3, out uint V4)
    {
        V1 = Seed + Prime1 + Prime2;
        V2 = Seed + Prime2;
        V3 = Seed;
        V4 = Seed - Prime1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint Round(uint Hash, uint Input)
    {
        return RotateLeft(Hash + (Input * Prime2), 13) * Prime1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint QueueRound(uint Hash, uint QueuedValue)
    {
        return RotateLeft(Hash + (QueuedValue * Prime3), 17) * Prime4;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint MixState(uint V1, uint V2, uint V3, uint V4)
    {
        return RotateLeft(V1, 1) + RotateLeft(V2, 7) + RotateLeft(V3, 12) + RotateLeft(V4, 18);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint MixEmptyState()
    {
        return Seed + Prime5;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint MixFinal(uint Hash)
    {
        Hash ^= Hash >> 15;
        Hash *= Prime2;
        Hash ^= Hash >> 13;
        Hash *= Prime3;
        Hash ^= Hash >> 16;

        return Hash;
    }

    private void Add(int Value)
    {
        uint Val = (uint)Value;
        uint PreviousLength = this.Length++;
        uint Position = PreviousLength % 4;

        if (Position == 0)
        {
            this.Queue1 = Val;
        }
        else if (Position == 1)
        {
            this.Queue2 = Val;
        }
        else if (Position == 2)
        {
            this.Queue3 = Val;
        }
        else
        {
            if (PreviousLength == 3)
            {
                Initialize(out this.V1, out this.V2, out this.V3, out this.V4);
            }

            this.V1 = Round(this.V1, this.Queue1);
            this.V2 = Round(this.V2, this.Queue2);
            this.V3 = Round(this.V3, this.Queue3);
            this.V4 = Round(this.V4, Val);
        }
    }

    /// <summary>
    ///     Gets the resulting hashcode from the current instance.
    /// </summary>
    /// <returns>
    ///     The resulting hashcode from the current instance.
    /// </returns>
    public readonly int ToHashCode()
    {
        uint Length = this.Length;
        uint Position = Length % 4;
        uint Hash = Length < 4 ? MixEmptyState() : MixState(this.V1, this.V2, this.V3, this.V4);

        Hash += Length * 4;

        if (Position > 0)
        {
            Hash = QueueRound(Hash, this.Queue1);

            if (Position > 1)
            {
                Hash = QueueRound(Hash, this.Queue2);

                if (Position > 2)
                {
                    Hash = QueueRound(Hash, this.Queue3);
                }
            }
        }

        Hash = MixFinal(Hash);
        return (int)Hash;
    }

    /// <inheritdoc/>
    [Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.", error: true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override readonly int GetHashCode() => throw new NotSupportedException();

    /// <inheritdoc/>
    [Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes.", error: true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override readonly bool Equals(object? @object) => throw new NotSupportedException();

    /// <summary>
    ///     Rotates the specified value left by the specified number of bits.
    ///     Similar in behavior to the x86 instruction ROL.
    /// </summary>
    /// <param name="Value">
    ///     The value to rotate.
    /// </param>
    /// <param name="Offset">
    ///     The number of bits to rotate by.
    ///     Any value outside the range [0..31] is treated as congruent mod 32.
    /// </param>
    /// <returns>
    ///     The rotated value.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint RotateLeft(uint Value, int Offset)
    {
        return (Value << Offset) | (Value >> (32 - Offset));
    }
}
