using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Julien.Script
{

    [Serializable]
    public class Round
    {
        public int CurrentRound;
        public int NumberOfPoint;
        public float TimeBeforeNextRound;
        
        public int _zombieToKillCount;
        public List<GameObject> _zombiesPrefabCanSpawn = null;
        public List<GameObject> _zombies;
        public List<GameObject> _zombieToKill;
    }
    
    [Serializable]
    public class DictionaryRound
    {
        public int Round;
        public GameObject ZombiePrefab;
    }
    
    public class RoundHundler : MonoBehaviour
    {
        public float MinSpawnRate;
        public float MaxSpawnRate;

        public Round Round;
        public List<DictionaryRound> RoundDictionary = new List<DictionaryRound>();
        
        [SerializeField] private GameObject[] _spawners;

        [SerializeField] private bool _inBreak;
        [SerializeField] private Break _break;

        public int ZombieToKillCount
        {
            get => Round._zombieToKillCount;
            set
            {
                Round._zombieToKillCount = value;
                if (Round._zombieToKillCount >= Round._zombieToKill.Count && !_inBreak)
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
            for (int i = Round.NumberOfPoint; i > 0;)
            {
                GameObject zombieToAdd = Round._zombiesPrefabCanSpawn[Random.Range(0, Round._zombiesPrefabCanSpawn.Count)].gameObject;
                int priceZombie = zombieToAdd.GetComponent<global::Julien.Script.ZombieScript.Zombie>().TypeZombie.PriceZombie;
                
                i -= priceZombie;
                //Debug.Log(i + " - "  + " prix : " + zombieToAdd.GetComponent<Zombie.Zombie>().TypeZombie.PirceZombie);
                Round._zombies.Add(zombieToAdd);
            }
            
            Round._zombieToKill = new List<GameObject>(Round._zombies);
            StartCoroutine("Spawn");
        }
        
        private IEnumerator Spawn()
        {
            for (int i = Round._zombies.Count; i > 0; i--)
            {
                GameObject spawner = _spawners[Random.Range(0, _spawners.Length)];
                int index = Random.Range(0,Round._zombies.Count);
                GameObject zombieToSpawn = Round._zombies[index].gameObject;

                yield return new WaitForSeconds(Random.Range(MinSpawnRate, MaxSpawnRate));
                
                Instantiate(zombieToSpawn, spawner.gameObject.transform.position, quaternion.identity, _parentZombie.transform);
                Round._zombies.Remove(zombieToSpawn);
            }
        }

        private void FirstRound()
        {
            Round.CurrentRound++;
            Round.NumberOfPoint += 10;
            
            AddZombieType();
            ChoiseEnemyToSpawn();
        }
        
         public IEnumerator Break()
         {
             _inBreak = true;
             Debug.Log("Take a break");
             yield return new WaitForSeconds(Round.TimeBeforeNextRound);
             Debug.Log("End of break");
             _inBreak = false;
             NexRound();
         }
        
        [ContextMenu("NextRound")]
        public void NexRound()
        {
            Round.CurrentRound++;
            Round.NumberOfPoint += 5;
            ZombieToKillCount = 0;
            AddZombieType();
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
            
            Round._zombieToKill.Clear();
            Round._zombies.Clear();
            AddZombieType();
        }

        private void AddZombieType()
        {
            foreach (var round in RoundDictionary)
            {
                if (round.Round <= Round.CurrentRound && !Round._zombiesPrefabCanSpawn.Contains(round.ZombiePrefab))
                {
                    Round._zombiesPrefabCanSpawn.Add(round.ZombiePrefab);
                }
            }
        }
    }
}
