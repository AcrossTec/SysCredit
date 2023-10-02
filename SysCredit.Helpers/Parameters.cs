namespace SysCredit.Helpers;

using System.Collections.Generic;

/// <summary>
///     Referencia: <see cref="ContextData" />.
/// </summary>
public static class Parameters
{
    /// <summary>
    ///     Referencia: <see cref="ContextData.KeyContext" />.
    /// </summary>
    public class KeyParameter
    {
        /// <summary>
        ///     Referencia: <see cref="ContextData.KeyContext.Params" />.
        /// </summary>
        private readonly ValueParameter Params;

        /// <summary>
        ///     Referencia: <see cref="ContextData.KeyContext.Key" />.
        /// </summary>
        private readonly string Key;

        /// <summary>
        ///     Referencia: <see cref="ContextData.KeyContext.KeyContext(string)" />.
        /// </summary>
        public KeyParameter(string Key)
        {
            this.Key = Key;
            this.Params = new ValueParameter();
        }

        /// <summary>
        ///     Referencia: <see cref="ContextData.KeyContext.KeyContext(ContextData.KeyContext.ValueContext, string)" />.
        /// </summary>
        private KeyParameter(ValueParameter Params, string key)
        {
            this.Key = key;
            this.Params = Params;
        }

        /// <summary>
        ///     Referencia: <see cref="ContextData.KeyContext.Value(object)" />.
        /// </summary>
        public ValueParameter Value(object Value)
        {
            Params.Add(Key, Value);
            return Params;
        }

        /// <summary>
        ///     Referencia: <see cref="ContextData.KeyContext.ValueContext" />.
        /// </summary>
        public class ValueParameter : Dictionary<string, object>
        {
            /// <summary>
            ///     Referencia: <see cref="ContextData.KeyContext.ValueContext.Key(string)" />.
            /// </summary>
            public KeyParameter Key(string Key)
            {
                return new KeyParameter(this, Key);
            }
        }
    }

    /// <summary>
    ///     Referencia: <see cref="ContextData.Key(string)" />.
    /// </summary>
    public static KeyParameter Key(string Key)
    {
        return new KeyParameter(Key);
    }
}
