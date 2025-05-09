using System.Collections.Generic;
using Julien.Script.Interface;
using Unity.AI.Navigation;
using UnityEngine;

namespace Julien.Script
{
    public class BreakSpawning : MonoBehaviour
    {
        public int NumberTurel;
        public int NumberBonus;
        
        public float MaxTimer;
        public float CurrentTimer;
        [SerializeField] private float _breakTimer;
        
        [SerializeField] private GameObject _turelOnGroundPrefab;
        [SerializeField] private GameObject _bonusTurelsPrefab;
        
        [SerializeField] private List<GameObject> _objectsToSpawn = new List<GameObject>();

        [SerializeField] private GameObject _parentSpawn;
        [SerializeField] private NavMeshSurface _navMeshSurface;
        private RoundHundler _roundHundler;

        private void Awake()
        {
            _roundHundler = GetComponent<RoundHundler>();
        }

        public void InBreakTime()
        {
            RandomGifts();
            SetObjectToSpawn();
            SetTimer();
        }

        public void RandomGifts()
        {
            float randomTurel = Random.Range(0f, 100f);
            if (randomTurel >= 50 && randomTurel < 80) NumberTurel = 1;
            if (randomTurel >= 80 && randomTurel <= 100) NumberTurel = 2;
            
            NumberBonus = Random.Range(2, 8);
        }

        public void SetObjectToSpawn()
        {
            for (int i = 0; i < NumberTurel; i++)
            {
                GameObject turel = _turelOnGroundPrefab;
                _objectsToSpawn.Add(turel);
            }
            for (int i = 0; i < NumberBonus; i++)
            {
                GameObject bonus = _bonusTurelsPrefab;
                _objectsToSpawn.Add(bonus);
            }
        }
        public void SetTimer()
        {
            MaxTimer = (_roundHundler.Round.TimeBeforeNextRound - 10) / _objectsToSpawn.Count;
            _breakTimer = _roundHundler.Round.TimeBeforeNextRound;
            CurrentTimer = 2;
        }
        
        private void Update()
        {
            if (_roundHundler.InBreak)
            {
                CurrentTimer -= Time.deltaTime;
                _breakTimer -= Time.deltaTime;
                if (CurrentTimer <= 0 && _objectsToSpawn.Count > 0)
                {
                    Spawn();
                    CurrentTimer = MaxTimer;
                }
                if (_breakTimer <= 0)
                {
                    _breakTimer = _roundHundler.Round.TimeBeforeNextRound;
                    CurrentTimer = MaxTimer;
                    EndBreakTime();
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

            GameObject ObjectToSpawn = Instantiate(_objectsToSpawn[0], new Vector3(RandomX, _parentSpawn.transform.position.y, RandomZ), Quaternion.identity, _parentSpawn.transform);
            ObjectToSpawn.GetComponent<IRandom>().Random();
            _objectsToSpawn.Remove(_objectsToSpawn[0]); 
        }

        public void EndBreakTime()
        {
            for (int i = 0; i < _parentSpawn.transform.childCount; i++)
            {
                Destroy(_parentSpawn.transform.GetChild(i).gameObject);
            }
        }
        
        public bool checkIfCanSpawn()
        {
            return true;
        }
    }
}
