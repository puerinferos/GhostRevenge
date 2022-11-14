using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text counter;
    private int enemyCount;
    private SignalBus _signalBus;

    private void Awake()
    {
    }

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<EnemyKilledSignal>(UpdateCounter);
    }
    private void UpdateCounter()
    {
        Debug.Log($"qweqwe");
        ++enemyCount;
        counter.text = enemyCount.ToString();
    }
}
