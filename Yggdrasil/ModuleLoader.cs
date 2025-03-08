using System;
using Cysharp.Threading.Tasks;
using Framework.Yggdrasil.Services;
using UnityEngine;

namespace Framework.Yggdrasil
{
    public class ModuleLoader : MonoBehaviour
    {
        private async void Awake()
        {
            try
            {
                Injector.Instance = new ServiceInjector();
                await RegisterService();
            }
            catch (Exception e)
            {
                throw new UnityException("Failed to register yggdrasil service.", e);
            }
        }

        protected virtual async UniTask RegisterService()
        {
            await Injector.Instance.Register<ILoggerService, FakeLoggerService>();
            await Injector.Instance.Register<IEventService, FakeEventService>();
            await Injector.Instance.Register<IResourcesService, FakeResourcesService>();
            await Injector.Instance.Register<IUIService, FakeUIService>();
            await Injector.Instance.Register<IConfigService, FakeConfigService>();
        }

        protected virtual void DeregisterService()
        {
            Injector.Instance.Deregister<ILoggerService, FakeLoggerService>();
            Injector.Instance.Deregister<IEventService, FakeEventService>();
            Injector.Instance.Deregister<IResourcesService, FakeResourcesService>();
            Injector.Instance.Deregister<IUIService, FakeUIService>();
            Injector.Instance.Deregister<IConfigService, FakeConfigService>();
        }

        protected void OnDestroy()
        {
            DeregisterService();
        }
    }
}