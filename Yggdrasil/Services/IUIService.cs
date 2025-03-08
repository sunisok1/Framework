using System;
using UnityEngine;

namespace Framework.Yggdrasil.Services
{
    public class UIPathAttribute : Attribute
    {
        public readonly string path;

        public UIPathAttribute(string path)
        {
            this.path = path;
        }
    }

    public interface IUIService : IService
    {
        void Open<T>() where T : UIBase;
        void Close<T>()where T : UIBase;
    }

    public abstract class UIBase : MonoBehaviour
    {
    }
    
    internal class FakeUIService: IUIService
    {
        public void Open<T>() where T : UIBase
        {
            throw new NotImplementedException();
        }

        public void Close<T>() where T : UIBase
        {
            throw new NotImplementedException();
        }
    }
}