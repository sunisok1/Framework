﻿using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Framework.Yggdrasil.ServiceImpl
{
    public class ResourcesService : IResourcesService
    {
        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public async UniTask<T> LoadAsync<T>(string path) where T : Object
        {
            return await Resources.LoadAsync(path, typeof(T)).ToUniTask() as T;
        }
    }
}