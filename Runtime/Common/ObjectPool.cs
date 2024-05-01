using System;
using System.Collections.Generic;

namespace ZaroDev.Utils.Runtime.Common
{
    public class ObjectPool<T>
    {
        private readonly List<T> _pool;
        private readonly Func<T> _factoryMethod;
        private readonly bool _isDynamic;
        private readonly Action<T> _turnOnCallback;
        private readonly Action<T> _turnOffCallback;


        public ObjectPool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, int initialStock = 0, bool isDynamic = true)
        {
            _factoryMethod = factoryMethod;
            _isDynamic = isDynamic;

            _turnOffCallback = turnOffCallback;
            _turnOnCallback = turnOnCallback;

            _pool = new List<T>();

            for (var i = 0; i < initialStock; i++)
            {
                var o = _factoryMethod();
                _turnOffCallback(o);
                _pool.Add(o);
            }
        }

        public T Get()
        {
            var result = default(T);
            if (_pool.Count > 0)
            {
                result = _pool[0];
                _pool.RemoveAt(0);
            }
            else if (_isDynamic)
            {
                result = _factoryMethod();
            }
            _turnOnCallback(result);
            return result;
        }

        public void Return(T o)
        {
            _turnOffCallback(o);
            _pool.Add(o);
        }
    }
}