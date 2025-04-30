using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public class RoundHundler : MonoBehaviour
    {
        public float MinSpawnRate;
        public float MaxSpawnRate;

        public float TimeBeforeNextRound;
        public int CurrentWave;
        public int NumberOfPoint;

        [SerializeField] private GameObject[] _spawners;

        [SerializeField] private List<GameObject> _zombiePrefabs;
        [SerializeField] private List<GameObject> _zombiesPrefabCanSpawn = null;

        [SerializeField] private List<GameObject> _zombies;
        [SerializeField] private List<GameObject> _zombieToKill;
        private int _zombieToKillCount;
        [SerializeField] private bool _inBreak;
        [SerializeField] private Break _break;

        public int ZombieToKillCount
        {
            get => _zombieToKillCount;
            set
            {
                _zombieToKillCount = value;
                if (_zombieToKillCount >= _zombieToKill.Count && !_inBreak)
                {
                    StartCoroutine("Break");
                }
            }
        }
        
        [SerializeField] private GameObject _parentZombie;
        private void Start()
        {
            _spawners = GameObject.FindGameObjectsWithTag("Spawner");
            FirstRound();
        }
        private void ChoiseEnemyToSpawn()
        {
            for (int i = NumberOfPoint; i > 0;)
            {
                GameObject zombieToAdd = _zombiePrefabs[Random.Range(0, _zombiePrefabs.Count)].gameObject;
                int priceZombie = zombieToAdd.GetComponent<ZombieScript.Zombie>().TypeZombie.PirceZombie;
                
                i -= priceZombie;
                //Debug.Log(i + " - "  + " prix : " + zombieToAdd.GetComponent<Zombie.Zombie>().TypeZombie.PirceZombie);
                _zombies.Add(zombieToAdd);
            }
            
            _zombieToKill = new List<GameObject>(_zombies);
            StartCoroutine("Spawn");
        }
        
        private IEnumerator Spawn()
        {
            for (int i = _zombies.Count; i > 0; i--)
            {
                GameObject spawner = _spawners[Random.Range(0, _spawners.Length)];
                int index = Random.Range(0,_zombies.Count);
                GameObject zombieToSpawn = _zombies[index].gameObject;

                yield return new WaitForSeconds(Random.Range(MinSpawnRate, MaxSpawnRate));
                
                Instantiate(zombieToSpawn, spawner.gameObject.transform.position, quaternion.identity, _parentZombie.transform);
                _zombies.Remove(zombieToSpawn);
            }
        }

        private void FirstRound()
        {
            CurrentWave++;
            NumberOfPoint += 10;
            
            ChoiseEnemyToSpawn();
        }
        
         public IEnumerator Break()
         {
             _inBreak = true;
             Debug.Log("Take a break");
             yield return new WaitForSeconds(TimeBeforeNextRound);
             Debug.Log("End of break");
             _inBreak = false;
             NexRound();
         }
        
        [ContextMenu("NextRound")]
        public void NexRound()
        {
            CurrentWave++;
            NumberOfPoint += 5;
            ZombieToKillCount = 0;
            Debug.Log("NexRound");
            
            ChoiseEnemyToSpawn();
        }

        [ContextMenu("SkipRound (debug)")]
        private void SkipRound()
        {
            GameObject[] zombiesOnMap = GameObject.FindGameObjectsWithTag("Zombie");
            foreach (GameObject zombie in zombiesOnMap)
            {
                Destroy(zombie);
            }

            StopCoroutine("Spawn");
            StartCoroutine("Break");
            
            _zombieToKill.Clear();
            _zombies.Clear();
        }
    }
}
