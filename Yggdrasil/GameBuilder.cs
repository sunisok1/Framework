namespace Framework.Yggdrasil
{
    public interface IGameService : IService
    {
        public void Run();

        public void OnApplicationFocus(bool focus)
        {
        }
    }
}