using System;

namespace Groot
{
    public class GrootFieldAttribute : Attribute
    {
        private readonly string _fieldNames;
        
        public GrootFieldAttribute(string fieldNames)
        {
            this._fieldNames = fieldNames;
        }

        public string GetGrootFields()
        {
            return this._fieldNames;
        }
    }
}