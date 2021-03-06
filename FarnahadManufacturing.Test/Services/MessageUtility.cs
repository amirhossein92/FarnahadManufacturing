﻿using System;

namespace FarnahadManufacturing.Test.Services
{
    public static class MessageUtility<T>
    {
        public static event Action<T> _messageAction;

        public static void InvokeMethod(T parameter)
        {
            _messageAction?.Invoke(parameter);
        }

        public static void Subscribe(Action<T> action)
        {
            _messageAction += action;
        }

        public static void UnSubscribe(Action<T> action)
        {
            _messageAction -= action;
        }
    }
}
