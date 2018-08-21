using System;

namespace Groot
{
    public struct ValueFromCsvType
    {
        private readonly string _value;

        public ValueFromCsvType(string value)
        {
            this._value = value;
        }
        
        public static implicit operator string(ValueFromCsvType valueFromCsv)
        {
            return valueFromCsv._value.ToString();
        }

        public static implicit operator int(ValueFromCsvType valueFromCsv)
        {
            return Convert.ToInt32(valueFromCsv._value);
        }
        public static implicit operator int?(ValueFromCsvType valueFromCsv)
        {
            return (int) valueFromCsv;
        }
        
        
        public static implicit operator long(ValueFromCsvType valueFromCsv)
        {
            return Convert.ToInt64(valueFromCsv._value);
        }
        public static implicit operator long?(ValueFromCsvType valueFromCsv)
        {
            return (long) valueFromCsv;
        }
        
        
        public static implicit operator decimal(ValueFromCsvType valueFromCsv)
        {
            return Convert.ToDecimal(valueFromCsv._value);
        }
        public static implicit operator decimal?(ValueFromCsvType valueFromCsv)
        {
            return (decimal) valueFromCsv;
        }
        
        
        public static implicit operator double(ValueFromCsvType valueFromCsv)
        {
            return Convert.ToDouble(valueFromCsv._value);
        }
        public static implicit operator double?(ValueFromCsvType valueFromCsv)
        {
            return (double) valueFromCsv;
        }
        
        
        public static implicit operator bool(ValueFromCsvType valueFromCsv)
        {
            return Convert.ToBoolean(valueFromCsv._value);
        }
        public static implicit operator bool?(ValueFromCsvType valueFromCsv)
        {
            return (bool) valueFromCsv;
        }

    }
}