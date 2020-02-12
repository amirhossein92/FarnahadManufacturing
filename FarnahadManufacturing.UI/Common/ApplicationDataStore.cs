using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.UI.Common
{
    public static class ApplicationDataStore
    {
        private static Dictionary<string, object> _items = new Dictionary<string, object>();

        public static void SendData<T>(string key, T value) where T : class
        {
            if (!_items.ContainsKey(key))
                _items.Add(key, value);
            else
                throw new Exception("Key already exists...");
        }

        public static T GetData<T>(string key) where T : class
        {
            if (_items.ContainsKey(key))
            {
                var value = _items[key] as T;
                _items.Remove(key);
                return value;
            }

            return null;
        }

    }
}
