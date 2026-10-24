using Julien.Script.Data.Zombie;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

public class NewRoundManager : MonoBehaviour
{
    [Header("==== Difficulty Settings ====")]
    [Header("")]
    [SerializeField] private AnimationCurve _difficultyCurve;
    [SerializeField] private AnimationCurve _speedRoundCurveMin;
    [SerializeField] private AnimationCurve _speedRoundCurveMax;

    [Header("==== Zombie parameters ====")]
    [Header("Curve nomarl zombie count")]
    [SerializeField] private AnimationCurve _normalZombieCruveMin;
    [SerializeField] private AnimationCurve _normalZombieCruveMax;

    [Header("Curve big zombie count")]
    [SerializeField] private AnimationCurve _bigZombieCurveMin;
    [SerializeField] private AnimationCurve _bigZombieCurveMax;

    [Header("Curve kamikaz count")]
    [SerializeField] private AnimationCurve _kamikazZombieCruveMin;
    [SerializeField] private AnimationCurve _kamikazZombieCruveMax;
    

    

    [Header("==== Round state ====")]
    [Header("")]
    [SerializeField] private int _currentRound;
    [Header("")]
    [SerializeField] private float _zombieHealthMultiplier;
    [SerializeField] private int _normalZombieCount;
    [SerializeField] private int _bigZombieCount;
    [SerializeField] private int _kamikazZombieCount;
    [SerializeField] private float _totalSpawnDuration;
    [SerializeField] private List<ZombieData> _zombieDataRound;

    [SerializeField] private RoundParameters _currentRoundState;

    private void Start()
    {
        SetParameters();
    }

    [ContextMenu("Calculer les paramètres (SetParameters)")]
    private void SetParameters()
    {
        _zombieHealthMultiplier = _difficultyCurve.Evaluate(_currentRound);

        _normalZombieCount = Mathf.RoundToInt(Random.Range(_normalZombieCruveMin.Evaluate(_currentRound), _normalZombieCruveMax.Evaluate(_currentRound)));
        _bigZombieCount = Mathf.RoundToInt(Random.Range(_bigZombieCurveMin.Evaluate(_currentRound), _bigZombieCurveMax.Evaluate(_currentRound)));
        _kamikazZombieCount = Mathf.RoundToInt(Random.Range(_kamikazZombieCruveMin.Evaluate(_currentRound), _kamikazZombieCruveMax.Evaluate(_currentRound)));
        _totalSpawnDuration = Random.Range(_speedRoundCurveMin.Evaluate(_currentRound), _speedRoundCurveMax.Evaluate(_currentRound));
    }

    [ContextMenu("StartRound")]
    private void StartRound()
    {
        SetParameters();
        _currentRoundState = new RoundParameters(_currentRound, _zombieHealthMultiplier, _normalZombieCount, _bigZombieCount, _kamikazZombieCount, _totalSpawnDuration);
    }

    
}

[Serializable]
public struct RoundParameters
{
    public int CurrentRound;
    public float ZombieHealthMultiplier;
    public int NormalZombieCount;
    public int BigZombieCount;
    public int KamikazZombieCount;
    public float TimerSpawn;
    public RoundParameters(int currentRound, float zombieHealthMultiplier, int normalZombieCount, int bigZombieCount, int kamikazZombieCount, float totalSpawnDuration)
    {
        CurrentRound = currentRound;
        ZombieHealthMultiplier = zombieHealthMultiplier;
        NormalZombieCount = normalZombieCount;
        BigZombieCount = bigZombieCount;
        KamikazZombieCount = kamikazZombieCount;
        TimerSpawn = totalSpawnDuration;
    }
}
