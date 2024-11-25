using Cysharp.Threading.Tasks;
using Framework.Yggdrasil.Services;
using JetBrains.Annotations;
using UnityEngine;

namespace Framework.ServiceImpl
{
    [UsedImplicitly]
    public class ObjectService : IObjectService
    {
        private readonly IResourcesService m_resourcesService;
        private readonly ILoggerService m_loggerService;

        public ObjectService(
            IResourcesService resourcesService,
            ILoggerService loggerService)
        {
            m_resourcesService = resourcesService;
            m_loggerService = loggerService;
        }

        public void OnStart()
        {
        }

        public void OnDestroy()
        {
        }

        public T Create<T, TArgs>(Transform parent, TArgs args) where TArgs : CreateArgs where T : BaseObject<TArgs>
        {
            var prefab = m_resourcesService.Load<T>(args.Path);
            return InstantiatePrefab(parent, prefab, args);
        }

        public async UniTask<T> CreateAsync<T, TArgs>(Transform parent, TArgs args) where TArgs : CreateArgs where T : BaseObject<TArgs>
        {
            var prefab = await m_resourcesService.LoadAsync<T>(args.Path);
            return InstantiatePrefab(parent, prefab, args);
        }

        private T InstantiatePrefab<T, TArgs>(Transform parent, T prefab, TArgs args) where TArgs : CreateArgs where T : BaseObject<TArgs>
        {
            if (prefab == null)
            {
                m_loggerService.LogError($"Failed to load prefab at path: {args.Path}");
                return null;
            }

            var instance = Object.Instantiate(prefab, parent);
            instance.OnCreated(args);
            return instance;
        }
    }
}