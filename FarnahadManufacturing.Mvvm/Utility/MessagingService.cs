using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.Mvvm.Utility
{
    public static class MessagingService<T> where T : class
    {
        private static Action<T> _action;

        public static void Subscribe(Action<T> action)
        {
            _action += action;
        }

        public static void UnSubscribe(Action<T> action)
        {
            _action -= action;
        }

        public static void Sent(T model)
        {
            _action?.Invoke(model);
        }
    }
}
