using System;
using System.Collections.Generic;

namespace Framework.Yggdrasil.Services
{
    public interface IGameService : IService
    {
        public void Run();

        public void OnApplicationFocus(bool focus)
        {
        }
    }
}