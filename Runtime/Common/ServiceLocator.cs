using System;
using System.Collections.Generic;

namespace ZaroDev.Utils.Runtime
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        private Dictionary<Type, object> _registry = new Dictionary<Type, object>();

        public void Register<T>(T service)
        {
            _registry[typeof(T)] = service;
        }

        public T GetService<T>()
        {
            return (T)_registry[typeof(T)];
        }
    }
}