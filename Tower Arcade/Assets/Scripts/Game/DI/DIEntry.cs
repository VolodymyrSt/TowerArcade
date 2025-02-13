using System;
using UnityEngine;

namespace DI
{
    public abstract class DIEntry : IDisposable
    {
        protected DIContainer Container { get; }
        protected bool IsSingelton { get; set; }

        protected DIEntry() { }
        protected DIEntry(DIContainer container)
        {
            Container = container;
        }

        public T Resolve<T>()
        {
            return ((DIEntry<T>)this).Resolve();
        }

        public DIEntry AsSingle()
        {
            IsSingelton = true;
            return this;
        }

        public abstract void Dispose();
    }

    public class DIEntry<T> : DIEntry
    {
        private Func<DIContainer, T> Factory { get; }
        private T _instance;
        private IDisposable _disposableInstance;

        public DIEntry(DIContainer container, Func<DIContainer, T> factory) : base(container)
        {
            Factory = factory;
        }

        public DIEntry (T instance)
        {
            _instance = instance;

            if(_instance is IDisposable disposableInstance)
            {
                _disposableInstance = disposableInstance;
            }

            IsSingelton = true;
        }

        public T Resolve()
        {
            if (IsSingelton)
            {
                if (_instance == null)
                {
                    _instance = Factory(Container);

                    if (_instance is IDisposable disposableInstance)
                    {
                        _disposableInstance = disposableInstance;
                    }
                }
                return _instance;
            }
            return Factory(Container);
        }

        public override void Dispose()
        {
            _disposableInstance?.Dispose();
        }
    }

}
