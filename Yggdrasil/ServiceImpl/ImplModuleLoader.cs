using Cysharp.Threading.Tasks;
using Framework.ServiceImpl;
using Framework.Yggdrasil;

public class ImplModuleLoader : ModuleLoader
{
    protected override async UniTask RegisterService()
    {
        await Injector.Instance.Register<ILoggerService, UnityLogger>();
        await Injector.Instance.Register<IEventService, EventService>();
        await Injector.Instance.Register<IResourcesService, ResourcesService>();
        await Injector.Instance.Register<IUIService, UIService>();
        await Injector.Instance.Register<IConfigService, ConfigService>();
    }

    protected override void DeregisterService()
    {
        Injector.Instance.Deregister<ILoggerService, UnityLogger>();
        Injector.Instance.Deregister<IEventService, EventService>();
        Injector.Instance.Deregister<IResourcesService, ResourcesService>();
        Injector.Instance.Deregister<IUIService, UIService>();
        Injector.Instance.Deregister<IConfigService, ConfigService>();
    }
}