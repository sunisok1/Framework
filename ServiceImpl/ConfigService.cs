using System;
using System.Collections.Generic;
using System.IO;
using Framework.Yggdrasil.Services;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Serialization;

namespace Framework.ServiceImpl
{
    public class ConfigService : IConfigService
    {
        private readonly Dictionary<Type, IConfig> _configs = new();
        private readonly Dictionary<Type, Delegate> _listeners = new();
        public readonly string rootPath = "Assets/Configs";

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

        public T LoadConfig<T>(string path) where T : IConfig
        {
            var fullPath = Path.Combine(rootPath, $"{path}.json");
            var fileInfo = new FileInfo(fullPath);

            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException($"can't find {fullPath}.");
            }

            var json = File.ReadAllText(fullPath);
            var type = typeof(T);
            try
            {
                var config = (T)JsonConvert.DeserializeObject(json, type);
                _configs[type] = config;
                return config;
            }
            catch (Exception e)
            {
                throw new Exception($"can't load {fullPath}.json", e);
            }
        }

        public void SaveConfig<T>(T config, string path) where T : IConfig
        {
            var fullPath = Path.Combine(rootPath, $"{path}.json");
            FileInfo fileInfo = new(fullPath);
            if (fileInfo.Directory is { Exists: false })
            {
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            }

            var serializeObject = JsonConvert.SerializeObject(config);
            File.WriteAllText($"{fullPath}.json", serializeObject);
            _configs[typeof(T)] = config;
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