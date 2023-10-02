namespace SysCredit.Helpers;

using System.Collections.Generic;

/// <summary>
///     Implementación para crear un Diccionario:
///     <see cref="Dictionary{TKey, TValue}">Dictionary&lt;<see cref="string"/>, <see cref="object"/>&gt;</see>,
///     usando una implementación simple de Fluent Api.
/// </summary>
public static class ContextData
{
    /// <summary>
    ///     Contiene el Key actual y una referencia del diccionario que se está creando.
    /// </summary>
    public class KeyContext
    {
        /// <summary>
        ///     Diccionario que contiene todas las claves y valores.
        /// </summary>
        private readonly ValueContext Params;

        /// <summary>
        ///     Clave del registro actual.
        /// </summary>
        private readonly string Key;

        /// <summary>
        ///     Crear un <see cref="ValueContext" /> como parte de la primer llamada ha <see cref="ContextData.Key(string)" />.
        /// </summary>
        /// <param name="Key">
        ///     Clave del diccionario.
        /// </param>
        public KeyContext(string Key)
        {
            this.Key = Key;
            this.Params = new ValueContext();
        }

        /// <summary>
        ///     Crea un Key usando un diccionario existente.
        /// </summary>
        /// <param name="Params">
        ///     Diccionario donde se guardará el Value de <paramref name="key" />.
        /// </param>
        /// <param name="key">
        ///     Clave del diccionario.
        /// </param>
        private KeyContext(ValueContext Params, string key)
        {
            this.Key = key;
            this.Params = Params;
        }

        /// <summary>
        ///     Agrega el valor de <see cref="KeyContext.Key" />.
        /// </summary>
        /// <param name="Value">
        ///     Valor para el <see cref="KeyContext.Key" />.
        /// </param>
        /// <returns>
        ///     Regresa una referencia del diccionario que se está creando.
        /// </returns>
        public ValueContext Value(object Value)
        {
            Params.Add(Key, Value);
            return Params;
        }

        /// <summary>
        ///     Diccionario que posee todas las claves y valores.
        /// </summary>
        public class ValueContext : Dictionary<string, object>
        {
            /// <summary>
            ///     Crea un <see cref="KeyContext" /> para <paramref name="Key" />.
            /// </summary>
            /// <param name="Key">
            ///     Clave del diccionario.
            /// </param>
            /// <returns>
            ///     Regresa un <see cref="KeyContext" /> donde se establecerá el valor de <paramref name="Key" />.
            /// </returns>
            public KeyContext Key(string Key)
            {
                return new KeyContext(this, Key);
            }
        }
    }

    /// <summary>
    ///     Método que inicia la creación del diccionario.
    ///     Se debe llamar <see cref="KeyContext.Value(object)" /> inmediatamente.
    /// </summary>
    /// <param name="Key">
    ///     Clave del diccionario.
    /// </param>
    /// <returns>
    ///     Regresa un objeto <see cref="KeyContext" /> que contiene un <see cref="KeyContext.ValueContext" />
    ///     que se transforma al diccionario que se está creando de forma implícita.
    /// </returns>
    public static KeyContext Key(string Key)
    {
        return new KeyContext(Key);
    }
}
