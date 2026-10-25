using Julien.Script.Data.Zombie;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;
using System.Collections;
using Julien.Script.ZombieScript;
using Julien.Script.Static;
using Julien.Script;

public class NewRoundManager : MonoBehaviourSingleton<NewRoundManager>
{
    [Header("==== Difficulty Settings ====")]
    [Header("")]
    [SerializeField] private AnimationCurve _difficultyCurve;
    [SerializeField] private AnimationCurve _speedRoundCurveMin;
    [SerializeField] private AnimationCurve _speedRoundCurveMax;

    [Header("==== Zombie parameters ====")]
    [Header("Curve nomarl zombie count")]
    [SerializeField] private ZombieData _normalZombieData;
    [SerializeField] private AnimationCurve _normalZombieCruveMin;
    [SerializeField] private AnimationCurve _normalZombieCruveMax;

    [Header("Curve big zombie count")]
    [SerializeField] private ZombieData _bigZombieData;
    [SerializeField] private AnimationCurve _bigZombieCurveMin;
    [SerializeField] private AnimationCurve _bigZombieCurveMax;

    [Header("Curve kamikaz count")]

    [SerializeField] private ZombieData _kamikazZombieData;
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


    [Header("==== Current round state ====")]
    [Header("")]
    [SerializeField] private RoundParameters _currentRoundState;


    private GameObject[] _spawnedZombies;
    private GameObject _zombieParent;

    private void OnEnable()
    {
       EventBus.OnZombieDie += OnZombieDie;
    }

    private void OnDisable()
    {
        EventBus.OnZombieDie -= OnZombieDie;
    }

    private void Start()
    {
        SetParameters();
        _zombieParent = Instantiate(new GameObject("ZombiesParent"), transform);
        _spawnedZombies = GameObject.FindGameObjectsWithTag("Spawner");
    }

    [ContextMenu("Clear State (Debug)")]
    private void ClearState()
    {
        _currentRoundState = new RoundParameters();
    }

    [ContextMenu("Calculer les paramètres (Debug)")]
    private void SetParameters()
    {
        _zombieHealthMultiplier = _difficultyCurve.Evaluate(_currentRound);

        _normalZombieCount = Mathf.FloorToInt(Random.Range(_normalZombieCruveMin.Evaluate(_currentRound), _normalZombieCruveMax.Evaluate(_currentRound)));
        _bigZombieCount = Mathf.FloorToInt(Random.Range(_bigZombieCurveMin.Evaluate(_currentRound), _bigZombieCurveMax.Evaluate(_currentRound)));
        _kamikazZombieCount = Mathf.FloorToInt(Random.Range(_kamikazZombieCruveMin.Evaluate(_currentRound), _kamikazZombieCruveMax.Evaluate(_currentRound)));
        _totalSpawnDuration = Random.Range(_speedRoundCurveMin.Evaluate(_currentRound), _speedRoundCurveMax.Evaluate(_currentRound));

        _currentRoundState = new RoundParameters(_currentRound, _zombieHealthMultiplier, _normalZombieCount,
       _bigZombieCount, _kamikazZombieCount, _totalSpawnDuration, _normalZombieData, _bigZombieData, _kamikazZombieData);
    }

    [ContextMenu("StartRound")]
    public void StartRound()
    {
        SetParameters();
        _currentRoundState = new RoundParameters(_currentRound, _zombieHealthMultiplier, _normalZombieCount, 
        _bigZombieCount, _kamikazZombieCount, _totalSpawnDuration, _normalZombieData, _bigZombieData, _kamikazZombieData);

        StartCoroutine(SpawnZombies());
    }

    [ContextMenu("SkipRound (Debug)")]
    private void SkipRound()
    {

        for (int i = 0; i < _zombieParent.transform.childCount; i++)
        {
            Destroy(_zombieParent.transform.GetChild(i).gameObject);
        }
        StopAllCoroutines();
        EndRound();
    }

    private IEnumerator SpawnZombies()
    {
        Debug.Log("SpawnZombie 1");
        float timeBetweenSpawns = _currentRoundState.TimerSpawn / _currentRoundState.TotalZombieCount;
        for (int i = 0; i < _currentRoundState.TotalZombieCount; i++)
        {
            Debug.Log("SpawnZombie 2");
            SpawnRandomZombie();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private void EndRound()
    {
        Debug.Log("EndRound");
        _currentRound++;
        NewBreakManager.Instance.StartBreak();
    }

    private void SpawnRandomZombie()
    {
        int randIndex = Random.Range(0, _currentRoundState.ZombieDataRound.Count);
        ZombieData zombieToSpawn = _currentRoundState.ZombieDataRound[randIndex];
        _currentRoundState.ZombieDataRound.RemoveAt(randIndex);

        GameObject zombie = Instantiate(zombieToSpawn.Prefab, GetRandomSpawner().transform.position, Quaternion.identity, _zombieParent.transform);
        zombie.GetComponent<Zombie>().SetUp(zombieToSpawn.MaxHealth * _currentRoundState.ZombieHealthMultiplier);
    }

    private GameObject GetRandomSpawner()
    {
        int index = Random.Range(0, _spawnedZombies.Length);
        return _spawnedZombies[index];
    }

    private void OnZombieDie()
    {
        if (_currentRoundState.ZombieDataRound.Count <= 0)
        {
           EndRound();
        }
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

    public int TotalZombieCount;

    public List<ZombieData> ZombieDataRound;

    private ZombieData _normalZombieData;
    private ZombieData _bigZombieData;
    private ZombieData _kamikazZombieData;

    public RoundParameters(int currentRound, float zombieHealthMultiplier, int normalZombieCount,
        int bigZombieCount, int kamikazZombieCount, float totalSpawnDuration, ZombieData normalZombieData,
        ZombieData bigZombieData, ZombieData kamikazZombieData)
    {
        CurrentRound = currentRound;
        ZombieHealthMultiplier = zombieHealthMultiplier;
        NormalZombieCount = normalZombieCount;
        BigZombieCount = bigZombieCount;
        KamikazZombieCount = kamikazZombieCount;
        TimerSpawn = totalSpawnDuration;
        TotalZombieCount = normalZombieCount + bigZombieCount + kamikazZombieCount;

        _normalZombieData = normalZombieData;
        _bigZombieData = bigZombieData;
        _kamikazZombieData = kamikazZombieData;

        ZombieDataRound = new List<ZombieData>();
        for (int i = 0; i < NormalZombieCount; i++)
        {
            ZombieDataRound.Add(_normalZombieData);
        }
        for (int i = 0; i < BigZombieCount; i++)
        {
            ZombieDataRound.Add(_bigZombieData);
        }
        for (int i = 0; i < KamikazZombieCount; i++)
        {
            ZombieDataRound.Add(_kamikazZombieData);
        }

    }
}
