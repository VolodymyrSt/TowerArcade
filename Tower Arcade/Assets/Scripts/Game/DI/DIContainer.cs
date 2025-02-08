using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DI
{
    public class DIContainer
    {
        private DIContainer _parentContainer;

        private readonly Dictionary<Type, DIEntry> _entriesMap = new Dictionary<Type, DIEntry>();
        private readonly HashSet<Type> _resolutionsCache = new HashSet<Type>();

        public DIContainer(DIContainer parentContainer = null)
        {
            _parentContainer = parentContainer;
        }

        public DIEntry RegisterFactory<T>(Func<DIContainer, T> factory)
        {
            var key = typeof(T);

            if (_entriesMap.ContainsKey(key))
            {
                throw new Exception(
                    $"DI: Factory with type {key.FullName} has already registered");
            }

            var diEntry = new DIEntry<T>(this, factory);

            _entriesMap[key] = diEntry;

            return diEntry;
        }

        public void RegisterInstance<T>(T instance)
        {
            var key = typeof(T);

            if(_entriesMap.ContainsKey(key))
            {
                throw new Exception(
                    $"DI: Instance with type {key.FullName} has already registered");
            }

            var diEntry = new DIEntry<T>(instance);

            _entriesMap[key] = diEntry;
        }

        public T Resolve<T>()
        {
            var key = typeof(T);

            if (_resolutionsCache.Contains(key))
            {
                throw new Exception($"DI: Cyclic dependency for type {key.FullName}");
            }

            _resolutionsCache.Add(key);

            try
            {
                if(_entriesMap.TryGetValue(key, out var diEntry))
                {
                    return diEntry.Resolve<T>();
                }

                if(_parentContainer != null)
                {
                    return _parentContainer.Resolve<T>();
                }
            }

            finally
            {
                _resolutionsCache.Remove(key);
            }

            throw new Exception($"Couldn't find dependency for type {key.FullName}");
        }

        public void UnRegister<T>(T instance)
        {
            var key = typeof(T);

            if (_entriesMap.ContainsKey(key))
            {
                _entriesMap.Remove(key);
            }
            else
            {
                throw new Exception(
                    $"DI: Instance with type {key.FullName} doesnt exist");
            }
        }

        public void Dispose()
        {
            foreach (var diEntry in _entriesMap.Values)
            {
                diEntry.Dispose();
            }
        }
    }
}

