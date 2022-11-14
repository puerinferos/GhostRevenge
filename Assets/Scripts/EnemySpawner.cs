using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : ITickable,IInitializable
{
    private int _desiredEnemyCount;
    private int _enemyCount;
    private float _lastSpawnTime;
    private LevelBoundary _levelBoundary;
    
    readonly Enemy.Factory _enemyFactory;
    readonly Settings _settings;
    readonly SignalBus _signalBus;

    public EnemySpawner(
        Settings settings,
        LevelBoundary levelBoundary,
        SignalBus signalBus,
        Enemy.Factory enemyFactory)
    {
        _enemyFactory = enemyFactory;
        _levelBoundary = levelBoundary;
        _settings = settings;
        _signalBus = signalBus;

        _desiredEnemyCount = settings.DesiredEnemyCount;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<EnemyRemovedSignal>(OnEnemyKilled);
    }

    void OnEnemyKilled()
    {
        _enemyCount--;
    }

    public void Tick()
    {
        if (Time.realtimeSinceStartup - _lastSpawnTime > _settings.delayBetweenSpawn && _desiredEnemyCount > _enemyCount)
        {
            SpawnEnemy();
            _enemyCount++;
        }
    }

    private void SpawnEnemy()
    {
        float speed = Random.Range(_settings.MinSpeed, _settings.MaxSpeed);

        var enemyFacade = _enemyFactory.Create(speed,_settings.Direction);
        enemyFacade.transform.position = StartPosition();

        _lastSpawnTime = Time.realtimeSinceStartup;
    }

    private Vector3 StartPosition()
    {
        var posOnSide = Random.Range(0, 1.0f);
        return new Vector3(
            _levelBoundary.Left + posOnSide * _levelBoundary.Width,
            _levelBoundary.Top, 0);
    }

    [Serializable]
    public class Settings
    {
        public Vector3 Direction;
        public float MinSpeed;
        public float MaxSpeed;
        public int DesiredEnemyCount;
        public float delayBetweenSpawn;
    }
}