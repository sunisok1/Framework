using System;
using System.Collections.Generic;
using Framework.Yggdrasil.Services;

namespace Framework.ServiceImpl
{
    public class ConfigService : IConfigService
    {
        private readonly Dictionary<Type, IConfig> _configs = new();
        private readonly Dictionary<Type, Delegate> _listeners = new();
        public readonly string rootPath = "Assets/Configs";

        public void OnStart()
        {
        }

        public void OnDestroy()
        {
        }

        public T GetConfig<T>() where T : IConfig
        {
            return (T)_configs[typeof(T)];
        }

        public void SetConfig<T>(T config) where T : IConfig
        {
            _configs[typeof(T)] = config;
        }

        public void DeleteConfig<T>() where T : IConfig
        {
            _configs.Remove(typeof(T));
        }

        public void LoadConfig<T>(string path) where T : IConfig
        {
            throw new System.NotImplementedException();
        }

        public void SaveConfig<T>(string path) where T : IConfig
        {
            throw new System.NotImplementedException();
        }

        public void RefreshConfigs()
        {
            foreach (var (key, value) in _listeners)
            {
                value.DynamicInvoke(_configs[key]);
            }
        }

        public IEnumerable<IConfig> ListAllConfigs()
        {
            return _configs.Values;
        }

        public bool HasConfig<T>()
        {
            return _configs.ContainsKey(typeof(T));
        }

        public void AddConfigListener<T>(Action<T> listener) where T : IConfig
        {
            var type = typeof(T);
            if (!_listeners.TryAdd(type, listener))
            {
                var @delegate = _listeners[type];
                _listeners[type] = Delegate.Combine(@delegate, listener);
            }

            _listeners.Add(type, listener);
        }

        public void RemoveConfigListener<T>(Action<T> listener) where T : IConfig
        {
            var type = typeof(T);
            if (!_listeners.TryGetValue(type, out var @delegate)) return;

            if (@delegate.Equals(listener))
            {
                _listeners.Remove(type);
            }
            else
            {
                _listeners[type] = Delegate.Remove(@delegate, listener);
            }
        }
    }
}