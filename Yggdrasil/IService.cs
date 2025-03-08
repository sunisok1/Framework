using System;
using Cysharp.Threading.Tasks;

namespace Framework.Yggdrasil
{
    public interface IService
    {
        UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }

        void Dispose()
        {
        }
    }
}