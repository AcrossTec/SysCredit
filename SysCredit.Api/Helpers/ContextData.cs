namespace SysCredit.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ContextData
{
    public class KeyParam
    {
        private readonly ValueParam Params;
        private readonly string Key;

        public KeyParam(string Key)
        {
            this.Key = Key;
            this.Params = new ValueParam();
        }

        private KeyParam(ValueParam Params, string key)
        {
            this.Key = key;
            this.Params = Params;
        }

        public ValueParam Value(object Value)
        {
            Params.Add(Key, Value);
            return Params;
        }

        public class ValueParam : Dictionary<string, object>
        {
            public KeyParam Key(string Key)
            {
                return new KeyParam(this, Key);
            }

            public KeyParam Key<Type>()
            {
                return new KeyParam(this, typeof(Type).Name);
            }
        }
    }

    public static KeyParam Key(string Key)
    {
        return new KeyParam(Key);
    }
}
