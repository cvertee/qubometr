using UnityEngine;
using Zenject;

public class UiInstaller : MonoInstaller
{
    public StoreUI storeUIInstance;
    
    public override void InstallBindings()
    {
        Container.Bind<StoreUI>().FromInstance(storeUIInstance);
    }
}