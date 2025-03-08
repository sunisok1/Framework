using System;
using Framework.Yggdrasil;
using JetBrains.Annotations;
using UnityEngine;

namespace Framework.ServiceImpl
{
    [UsedImplicitly]
    public class UnityLogger : ILoggerService
    {
        private readonly ILogger logger = Debug.unityLogger;

        public void OnAdd()
        {
            logger.Log("UnityLogger OnStart");
        }

        public void Log(object message) => logger.Log(LogType.Log, message);
        public void LogWarning(object message) => logger.Log(LogType.Warning, message);
        public void LogError(object message) => logger.Log(LogType.Error, message);
        public void LogException(Exception exception) => logger.LogException(exception, null);
    }
}