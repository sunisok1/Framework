using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Framework.Yggdrasil
{
    public class ServiceInjector
    {
        private readonly Dictionary<Type, IService> m_services = new();

        public async UniTask Register<TInterface, T>() where TInterface : IService where T : TInterface, new() => await Register<TInterface>(new T());

        public async UniTask Register<TInterface>(TInterface t) where TInterface : IService
        {
            if (m_services.ContainsKey(typeof(TInterface)))
            {
                Deregister<TInterface, TInterface>();
            }

            await t.Initialize();
            m_services.Add(typeof(TInterface), t);
        }

        public void Deregister<TInterface, T>() where TInterface : IService where T : TInterface
        {
            var serviceInterface = typeof(TInterface);
            if (!m_services.TryGetValue(typeof(TInterface), out var service))
            {
                Console.WriteLine($"Warning: Service of type {serviceInterface.FullName} is either not registered or has already been deregistered.");
                return;
            }

            if (service is not T) return;

            service.Dispose();
            m_services.Remove(serviceInterface);
        }

        public T GetService<T>() where T : IService
        {
            if (!m_services.TryGetValue(typeof(T), out var service)) throw new NullReferenceException($"service {typeof(T)} is null");
            return (T)service;
        }
    }
}