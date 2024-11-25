using System;
using System.Collections.Generic;
using Framework.Yggdrasil;
using Framework.Yggdrasil.Services;
using JetBrains.Annotations;

namespace Framework.ServiceImpl
{
    [UsedImplicitly]
    public class EventService : IEventService
    {
        private readonly ILoggerService m_loggerService;
        private static readonly Dictionary<Type, Delegate> eventHandlers = new();

        [ServiceConstructor]
        public EventService(ILoggerService loggerService)
        {
            m_loggerService = loggerService;
        }
        
        public void OnStart()
        {
            m_loggerService.Log("EventService OnStart");
        }

        public void OnDestroy()
        {
        }

        public void Raise<T>(object sender, T args) where T : EventArgs
        {
            var type = typeof(T);
            if (!eventHandlers.TryGetValue(type, out var delegateObj)) return;
            foreach (var individualDelegate in delegateObj.GetInvocationList())
            {
                if (individualDelegate is not EventHandler<T> handler) continue;
                try
                {
                    handler(sender, args);
                }
                catch (Exception ex)
                {
                    m_loggerService.LogException(ex);
                }
            }
        }

        public void AddHandler<T>(EventType<T> eventType) where T : EventArgs
        {
            eventType.OnAdd();
            var type = typeof(T);
            eventHandlers.TryGetValue(type, out var existingDelegate);
            eventHandlers[type] = Delegate.Combine(existingDelegate, eventType.handler);
        }

        public void RemoveHandler<T>(EventType<T> eventType) where T : EventArgs
        {
            var type = typeof(T);
            if (!eventHandlers.TryGetValue(type, out var existingDelegate)) return;

            var newDelegate = Delegate.Remove(existingDelegate, eventType.handler);
            if (newDelegate == null)
            {
                eventHandlers.Remove(type);
            }
            else
            {
                eventHandlers[type] = newDelegate;
            }
        }
    }
}