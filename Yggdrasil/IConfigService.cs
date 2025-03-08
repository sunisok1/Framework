using System;
using System.Collections.Generic;

namespace Framework.Yggdrasil
{
    public interface IConfig
    {
    }

    public interface IConfigList<out T> : IConfig where T : class
    {
        IEnumerable<T> Values { get; }
    }

    public interface IConfigService : IService
    {
        T GetConfig<T>() where T : IConfig;
        void SetConfig<T>(T config) where T : IConfig;
        void DeleteConfig<T>() where T : IConfig;
        T LoadConfig<T>(string path) where T : IConfig;
        void SaveConfig<T>(T config,string path) where T : IConfig;
        void RefreshConfigs();
        IEnumerable<IConfig> ListAllConfigs();
        bool HasConfig<T>();
        void AddConfigListener<T>(Action<T> listener) where T : IConfig;
        void RemoveConfigListener<T>(Action<T> listener) where T : IConfig;
    }
    
    internal class FakeConfigService:IConfigService
    {
        public T GetConfig<T>() where T : IConfig
        {
            throw new NotImplementedException();
        }

        public void SetConfig<T>(T config) where T : IConfig
        {
            throw new NotImplementedException();
        }

        public void DeleteConfig<T>() where T : IConfig
        {
            throw new NotImplementedException();
        }

        public T LoadConfig<T>(string path) where T : IConfig
        {
            throw new NotImplementedException();
        }

        public void SaveConfig<T>(T config, string path) where T : IConfig
        {
            throw new NotImplementedException();
        }

        public void RefreshConfigs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IConfig> ListAllConfigs()
        {
            throw new NotImplementedException();
        }

        public bool HasConfig<T>()
        {
            throw new NotImplementedException();
        }

        public void AddConfigListener<T>(Action<T> listener) where T : IConfig
        {
            throw new NotImplementedException();
        }

        public void RemoveConfigListener<T>(Action<T> listener) where T : IConfig
        {
            throw new NotImplementedException();
        }
    }
}