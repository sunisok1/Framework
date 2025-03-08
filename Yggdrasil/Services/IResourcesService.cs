using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Framework.Yggdrasil.Services
{
    public interface IResourcesService : IService
    {
        T Load<T>(string path) where T : Object;

        UniTask<T> LoadAsync<T>(string path) where T : Object;
    }
    
    internal class FakeResourcesService: IResourcesService
    {
        public T Load<T>(string path) where T : Object
        {
            throw new System.NotImplementedException();
        }

        public UniTask<T> LoadAsync<T>(string path) where T : Object
        {
            throw new System.NotImplementedException();
        }
    }
}