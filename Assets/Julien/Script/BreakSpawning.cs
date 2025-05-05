using System.Collections.Generic;
using Julien.Script.Data.Upgrader;
using Julien.Script.TurelScripts;
using Script.Data.TurellData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Julien.Script
{
    public class BreakSpawning : MonoBehaviour
    {
        public int NumberTurel;
        public int NumberBonus;

        public float MaxTimer;
        public float CurrentTimer;
        
        [SerializeField] private GameObject _turelOnGroundPrefab;
        [SerializeField] private GameObject _bonusTurelsPrefab;
        
        [SerializeField] private List<Upgrader> _bonusPrefabsData = new List<Upgrader>();
        [SerializeField] private List<TurelData> _turelPrefabsData = new List<TurelData>();
        
        [SerializeField] private List<GameObject> _objectsToSpawn = new List<GameObject>();

        [SerializeField] private GameObject _parentSpawn;
        private RoundHundler _roundHundler;

        private void Awake()
        {
            _roundHundler = GetComponent<RoundHundler>();
        }

        private void Start()
        {
            InBreakTime();
            SetTimer();
        }

        public void InBreakTime()
        {
            SetObjectToSpawn();
        }

        public void SetObjectToSpawn()
        {
            for (int i = 0; i < NumberTurel; i++)
            {
                GameObject turel = _turelOnGroundPrefab;
                
                int index = Random.Range(0, _turelPrefabsData.Count);
                turel.GetComponent<TurelOnGround>().TurelWrap.TurelType = _turelPrefabsData[index];
                
                _objectsToSpawn.Add(_turelOnGroundPrefab.gameObject);
            }
            for (int i = 0; i < NumberBonus; i++)
            {
                GameObject bonus = _bonusTurelsPrefab;
                
                int index = Random.Range(0, _bonusPrefabsData.Count);
                bonus.GetComponent<BonusTurel>().UpgraderWrap.Upgrader = _bonusPrefabsData[index];
                
                _objectsToSpawn.Add(bonus.gameObject);
            }
        }
        
        public void SetTimer()
        {
            MaxTimer = (_roundHundler.Round.TimeBeforeNextRound - 10) / _objectsToSpawn.Count;
            CurrentTimer = 2;
        }
        
        private void Update()
        {
            if (_roundHundler.InBreak)
            {
                CurrentTimer -= Time.deltaTime;
                if (CurrentTimer <= 0 && _objectsToSpawn.Count > 0)
                {
                    Spawn();
                    CurrentTimer = MaxTimer;
                }
            }
        }

        public void Spawn()
        {
            BoxCollider box = _parentSpawn.GetComponent<BoxCollider>();
            
            float x = box.center.x + box.size.x / 2;
            float z = box.center.z + box.size.z / 2;
            
            float RandomX = Random.Range(-x, x);
            float RandomZ = Random.Range(-z, z);

            GameObject objectToSpawn = Instantiate(_objectsToSpawn[0], new Vector3(RandomX, _parentSpawn.transform.position.y, RandomZ), Quaternion.identity, _parentSpawn.transform);
            _objectsToSpawn.Remove(_objectsToSpawn[0]); 
        }
        
        public bool checkIfCanSpawn()
        {
            return true;
        }
    }
}
