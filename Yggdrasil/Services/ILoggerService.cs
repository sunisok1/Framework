using System;

namespace Framework.Yggdrasil.Services
{
    public interface ILoggerService : IService
    {
        void Log(object message);
        void LogWarning(object message);
        void LogError(object message);
        void LogException(Exception exception);
    }

    internal class FakeLoggerService : ILoggerService
    {
        public void Log(object message)
        {
        }

        public void LogWarning(object message)
        {
        }

        public void LogError(object message)
        {
        }

        public void LogException(Exception exception)
        {
        }
    }
}