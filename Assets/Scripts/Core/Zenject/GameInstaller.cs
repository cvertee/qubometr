using Game;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public AudioManager audioManagerPrefab;
    public Coin coinPrefab;
    public GameManager gameManagerInstance;
    public StoreManager storeManagerInstance;
    public StoreItemInfo storeItemInfoPrefab;
    
    public override void InstallBindings()
    {
        var audioManager = Container.InstantiatePrefabForComponent<AudioManager>(audioManagerPrefab);
        
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle();
        Container.Bind<CoinSpawner>().FromNew().AsSingle();
        Container.BindFactory<AudioManager, Coin, Coin.Factory>().FromComponentInNewPrefab(coinPrefab);
        Container.Bind<GameManager>().FromInstance(gameManagerInstance);
        Container.Bind<StoreManager>().FromInstance(storeManagerInstance).AsSingle();
        Container
            .BindFactory<StoreManager, StoreItemInfo, StoreItemInfo.Factory>()
            .FromComponentInNewPrefab(storeItemInfoPrefab);
    }
}