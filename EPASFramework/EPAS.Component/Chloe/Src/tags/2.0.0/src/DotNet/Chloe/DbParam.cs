using System;

namespace Chloe
{
    public class DbParam
    {
        string _name;
        object _value;
        Type _type;

        public DbParam()
        {
        }
        public DbParam(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
        public DbParam(string name, object value, Type type)
        {
            this.Name = name;
            this.Value = value;
            this.Type = type;
        }

        public string Name { get { return this._name; } set { this._name = value; } }
        public object Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
                if (value != null)
                    this._type = value.GetType();
            }
        }
        public byte? Precision { get; set; }
        public byte? Scale { get; set; }
        public int? Size { get; set; }
        public Type Type { get { return this._type; } set { this._type = value; } }

        public static DbParam Create<T>(string name, T value)
        {
            var param = new DbParam(name, value);
            if (value == null)
                param.Type = typeof(T);
            return param;
        }
        public static DbParam Create(string name, object value)
        {
            return new DbParam(name, value);
        }
        public static DbParam Create(string name, object value, Type type)
        {
            return new DbParam(name, value, type);
        }
    }
}
