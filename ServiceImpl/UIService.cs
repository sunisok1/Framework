using System;
using System.Collections.Generic;
using Framework.Yggdrasil.Services;
using JetBrains.Annotations;
using UnityEngine;

namespace Common
{
    [UsedImplicitly]
    public class UIService : IUIService
    {
        private readonly ILoggerService m_loggerService;
        private readonly IObjectService m_objectService;
        private Canvas canvas;

        private readonly Dictionary<Type, MonoBehaviour> uiDictionary = new();

        public UIService(ILoggerService loggerService, IObjectService objectService)
        {
            m_loggerService = loggerService;
            m_objectService = objectService;
        }

        public void OnStart()
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

        public void OnDestroy()
        {
        }

        public void Open<T, TArgs>(TArgs args) where T : BaseUI<TArgs> where TArgs : CreateArgs
        {
            var type = typeof(T);
            if (uiDictionary.ContainsKey(type))
            {
                m_loggerService.LogWarning($"UIManager: {type.Name} is already open, no need to open again.");
                return;
            }

            var instance = m_objectService.Create<T, TArgs>(canvas.transform, args);

            uiDictionary.Add(type, instance);
            m_loggerService.Log($"UIManager: Successfully opened {type.Name}.");
        }

        public void Close<T>()
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