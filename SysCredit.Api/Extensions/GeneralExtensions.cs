namespace SysCredit.Api.Extensions;

using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Security.Cryptography;
using System.Text;

/// <summary>
///     Métodos de utilería de propósito general.
/// </summary>
public static class GeneralExtensions
{
    /// <summary>
    ///     Realiza un casting de tipo <typeparamref name="TObject" /> sobre el objeto <paramref name="object" />.
    /// </summary>
    /// <typeparam name="TObject">
    ///     Tipo del casting ha realizar.
    /// </typeparam>
    /// <param name="object">
    ///     Objeto que se le aplicará el casting de tipo <typeparamref name="TObject" />.
    /// </param>
    /// <returns>
    ///     Regresa el objeto <paramref name="object" /> convertido al tipo <typeparamref name="TObject" />.
    /// </returns>
    /// <exception cref="InvalidCastException">
    ///     Excepción que es lanzada cuando <paramref name="object" /> no se puede convertir al tipo <typeparamref name="TObject" />.
    /// </exception>
    public static TObject? As<TObject>(this object? @object) => (TObject?)@object;

    /// <summary>
    ///     Se asume que <paramref name="object" /> es de tipo <see cref="IStore" /> o un derivado de este
    ///     y se realiza la conversión ha ese tipo.
    /// </summary>
    /// <param name="object">
    ///     Objeto que se convertirá al tipo <see cref="IStore" />.
    /// </param>
    /// <returns>
    ///     Regresa el objeto <paramref name="object" /> transformado al tipo <see cref="IStore" />.
    /// </returns>
    /// <exception cref="InvalidCastException">
    ///     Excepción que es lanzada cuando <paramref name="object" /> no se puede convertir al tipo <see cref="IStore" />.
    /// </exception>
    public static IStore AsStore(this object @object) => @object.As<IStore>()!;

    /// <summary>
    ///     Se asume que <paramref name="object" /> es de tipo <see cref="IStore{TModel}" /> o un derivado de este
    ///     y se realiza la conversión ha ese tipo.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo genérico de <see cref="IStore{TModel}" /> que sera usado.
    /// </typeparam>
    /// <param name="object">
    ///     Regresa el objeto <paramref name="object" /> transformado al tipo <see cref="IStore{TModel}" />.
    /// </param>
    /// <returns>
    ///     Regresa el objeto <paramref name="object" /> transformado al tipo <see cref="IStore{TModel}" />.
    /// </returns>
    /// <exception cref="InvalidCastException">
    ///     Excepción que es lanzada cuando <paramref name="object" /> no se puede convertir al tipo <see cref="IStore{TModel}" />.
    /// </exception>
    public static IStore<T> AsStore<T>(this object @object) where T : Entity => @object.As<IStore<T>>()!;

    /// <summary>
    ///     Permite escapar los caracteres especiales para una instrucción "Like" de SQL.
    /// </summary>
    /// <param name="Value">
    ///     Objeto que tiene el valor que se le aplicará la corrección.
    /// </param>
    /// <returns>
    ///     Regresa <paramref name="Value" /> con las correcciones correspondientes.
    /// </returns>
    public static string? EscapedLike(this string? Value)
    {
        StringBuilder Builder = new((Value?.Length ?? 0) * 2);

        foreach (var Char in Value.DefaultIfEmpty())
        {
            Builder.Append(Char switch
            {
                '[' => "[[]",
                '%' => "[%]",
                '_' => "[_]",
                _ => Char
            });
        }

        return Builder.ToString();
    }

    /// <summary>
    ///     Crea un código Hash del texto usado como argumento.
    /// </summary>
    /// <param name="Text">
    ///     Texto del que se obtendrá su código Hash.
    /// </param>
    /// <returns>
    ///     Regresa un array de bytes como resulto de convertir <paramref name="Text" /> en código Hash.
    /// </returns>
    public static byte[] ComputeHashSha512(this string Text)
    {
        return SHA512.HashData(Encoding.UTF8.GetBytes(Text));
    }
}
