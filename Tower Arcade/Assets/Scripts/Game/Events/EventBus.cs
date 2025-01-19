using System;
using System.Collections.Generic;

namespace Game
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<object>> _signals;

        public EventBus() => _signals = new Dictionary<Type, List<object>>();

        public void SubscribeEvent<T>(Action<T> callback)
        {
            var key = typeof(T);

            if (_signals.ContainsKey(key))
            {
                _signals[key].Add(callback);
            }
            else 
            { 
                _signals.Add(key, new List<object>() { callback });
            }
        }

        public void UnSubscribeEvent<T>(Action<T> callback)
        {
            var key = typeof(T);

            if (!_signals.ContainsKey(key))
                throw new Exception($"You are trying to unregister not existed event {key}");

            _signals[key].Remove(callback);
        }

        public void Invoke<T>(T signal)
        {
            var key = typeof(T);

            if (!_signals.ContainsKey(key))
                throw new Exception($"You are trying to invoke not register event {key}");

            foreach (var obj in _signals[key])
            {
                var myEvent = obj as Action<T>;
                myEvent?.Invoke(signal);
            }
        }

        public void ClearSignals() => _signals.Clear();
    }
}
