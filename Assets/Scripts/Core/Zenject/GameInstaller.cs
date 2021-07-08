using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public AudioManager audioManagerPrefab;
    
    public override void InstallBindings()
    {
        var audioManager = Container.InstantiatePrefabForComponent<AudioManager>(audioManagerPrefab);
        
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle();
    }
}