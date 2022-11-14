using System;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IPoolable<float,Vector3,IMemoryPool>, IDisposable
{
    private float _speed;
    private Vector3 _direction;
    
    private IMemoryPool _pool;
    private SignalBus _signalBus;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
    }

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Move()
    {
        _rb.velocity = _direction*_speed;
    }

    private void Update()
    {
        Move();
    }

    public void Die()
    {
        _signalBus.Fire<EnemyKilledSignal>();
        _signalBus.Fire<EnemyRemovedSignal>();
        Dispose();
    }

    public void Remove()
    {
        _signalBus.Fire<EnemyRemovedSignal>();
        Dispose();
    }

    public void Dispose()
    {
        _pool.Despawn(this);
    }

    public void OnDespawned()
    {
        _pool = null;
    }

    public void OnSpawned(float speed,Vector3 direction, IMemoryPool pool)
    {
        _pool = pool;
        _speed = speed;
        _direction = direction;
    }

    public class Factory : PlaceholderFactory<float, Vector3, Enemy>
    {
    }
}

public struct EnemyKilledSignal
{
}
public struct EnemyRemovedSignal
{
}