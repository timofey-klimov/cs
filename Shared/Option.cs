using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public struct Option<T>
    {
        private T? _value;
        private bool _isNull;

        public T? Value => _isNull ? null : _value;



        public static Option<T> Some(T value) => new Option<T> { _value = value,_isNull = false };
        public static Option<T> Null => new Option<T>() { _isNull = true };
    }
}
