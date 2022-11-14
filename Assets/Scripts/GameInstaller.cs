using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject]
    Settings _settings = null;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();

        Container.BindFactory<float, Vector3, Enemy, Enemy.Factory>()
            .FromPoolableMemoryPool<float, Vector3, Enemy, EnemyPool>(poolBinder => poolBinder
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_settings.EnemyFacadePrefab));
        
        Container.Bind<LevelBoundary>().AsSingle();
        Container.BindInterfacesTo<PlayerInput>().AsSingle();
        GameSignalsInstaller.Install(Container);
    }
    
    [Serializable]
    public class Settings
    {
        public GameObject EnemyFacadePrefab;
    }

    class EnemyPool : MonoPoolableMemoryPool<float, Vector3, IMemoryPool, Enemy>
    {
    }

}