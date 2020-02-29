using System;
using System.Collections.Generic;

namespace FarnahadManufacturing.Base.Common
{
    public static class ApplicationDataStore
    {
        private static Dictionary<string, object> _items = new Dictionary<string, object>();

        public static void SendData<T>(string key, T value)
        {
            if (!_items.ContainsKey(key))
                _items.Add(key, value);
            else
                throw new Exception("Key already exists...");
        }

        public static T GetData<T>(string key)
        {
            if (_items.ContainsKey(key))
            {
                var value = (T)_items[key];
                _items.Remove(key);
                return value;
            }

            return default(T);
        }

    }
}
