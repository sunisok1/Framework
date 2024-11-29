using System;
using Framework.Yggdrasil.Services;
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

        public void OnRemove()
        {
        }

        public void Log(string message) => logger.Log(LogType.Log, message);
        public void LogWarning(string message) => logger.Log(LogType.Warning, message);
        public void LogError(string message) => logger.Log(LogType.Error, message);
        public void LogException(Exception exception) => logger.LogException(exception, null);
    }
}