using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework.Yggdrasil.ServiceImpl
{
    [UsedImplicitly]
    public class UIService : IUIService
    {
        private readonly ILoggerService m_loggerService;
        private readonly IResourcesService m_resourcesService;
        private Canvas canvas;

        private readonly Dictionary<Type, MonoBehaviour> uiDictionary = new();

        public UIService()
        {
            m_loggerService = Injector.Instance.GetService<ILoggerService>();
            m_resourcesService = Injector.Instance.GetService<IResourcesService>();
        }

        public void OnAdd()
        {
            var gameObject = GameObject.Find("Canvas");
            if (gameObject == null)
            {
                gameObject = new GameObject("Canvas");
                canvas = gameObject.AddComponent<Canvas>();
            }
            else
            {
                canvas = gameObject.GetComponent<Canvas>();
            }
        }

        public void Open<T>() where T : UIBase
        {
            var type = typeof(T);
            if (uiDictionary.ContainsKey(type))
            {
                m_loggerService.LogWarning($"UIManager: {type.Name} is already open, no need to open again.");
                return;
            }

            var path = type.GetCustomAttribute<UIPathAttribute>().path;
            var prefab = m_resourcesService.Load<T>(path);
            var instance = Object.Instantiate(prefab, canvas.transform);

            uiDictionary.Add(type, instance);
            m_loggerService.Log($"UIManager: Successfully opened {type.Name}.");
        }

        public void Close<T>() where T : UIBase
        {
            var type = typeof(T);
            if (!uiDictionary.TryGetValue(type, out var ui))
            {
                m_loggerService.LogWarning($"UIManager: Attempting to close {type.Name}, but it was not found. It may not have been opened or has already been closed.");
                return;
            }

            UnityEngine.Object.Destroy(ui.gameObject);
            uiDictionary.Remove(type);
            m_loggerService.Log($"UIManager: Successfully closed {type.Name}.");
        }
    }
}