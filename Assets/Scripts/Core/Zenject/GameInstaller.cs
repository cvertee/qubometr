using Core;
using Data;
using Game;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public Item itemPrefab;
    public AudioManager audioManagerPrefab;
    public Coin coinPrefab;
    public GameManager gameManagerInstance;
    public StoreManager storeManagerInstance;
    public StoreItemInfo storeItemInfoPrefab;
    public CommandExecutor commandExecutorInstance;
    public GameSettingsSO defaultGameSettings;
    public ParticleEmitterSO particleEmitterInstance;
    public ItemDataSO itemDataInstance;
    public GameObjectDestroyer objectDestroyerInstance;
    public ScriptService scriptServiceInstance;
    
    public override void InstallBindings()
    {
        BindFactories();
        InstallManagers();
        InstallData();
        InstallOther();
    }

    private void InstallManagers()
    {
        var audioManager = Container.InstantiatePrefabForComponent<AudioManager>(audioManagerPrefab);
        
        Container
            .Bind<AudioManager>()
            .FromInstance(audioManager)
            .AsSingle();
        
        Container
            .Bind<GameManager>()
            .FromInstance(gameManagerInstance)
            .AsSingle();
        
        Container
            .Bind<StoreManager>()
            .FromInstance(storeManagerInstance)
            .AsSingle();
    }

    private void InstallData()
    {
        Container
            .Bind<GameSettingsSO>()
            .FromInstance(defaultGameSettings);
        
        Container
            .Bind<IItemDatabase>()
            .To<ItemDataSO>()
            .FromInstance(itemDataInstance);
    }
    
    private void InstallOther()
    {
        Container
            .Bind<CoinSpawner>()
            .FromNew()
            .AsSingle();
        
        Container
            .Bind<CommandExecutor>()
            .FromInstance(commandExecutorInstance);
        
        Container
            .Bind<IParticleEmitter>()
            .To<ParticleEmitterSO>()
            .FromInstance(particleEmitterInstance);
        
        Container
            .Bind<IObjectDestroyer>()
            .To<GameObjectDestroyer>()
            .FromInstance(objectDestroyerInstance);

        Container
            .Bind<ScriptService>()
            .FromInstance(scriptServiceInstance);
    }

    private void BindFactories()
    {
        Container
            .BindFactory<GameSettingsSO, Item, Item.Factory>()
            .FromComponentInNewPrefab(itemPrefab);
        
        Container
            .BindFactory<StoreManager, StoreItemInfo, StoreItemInfo.Factory>()
            .FromComponentInNewPrefab(storeItemInfoPrefab);
        
        Container
            .BindFactory<AudioManager, Coin, Coin.Factory>()
            .FromComponentInNewPrefab(coinPrefab);
    }
}