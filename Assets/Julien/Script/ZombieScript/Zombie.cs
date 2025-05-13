using System;
using System.Collections.Generic;
using System.Linq;
using Julien.Script.Data.Zombie;
using Julien.Script.PlayerScripts;
using Julien.Script.Static;
using Script;
using Script.ZombieScript.States;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Julien.Script.ZombieScript
{
    public class Zombie : MonoBehaviour
    {
        [Header("Data")]
        public ZombieData TypeZombie;
        public int Price;
        public float Damage;
        public float Speed;
        public float Acceleration;
        public float MaxHealth;
        public float AttackSpeed;
        public float AttackRange;
        
        public float CurrentHealth;

        [Header("Target")] 
        
        public List<GameObject> Targets;
        public GameObject Target;
        public string TargetTag;

        public ZombieState CurrentState;

        public NavMeshAgent NavMeshAgent;
        public Rigidbody Rigidbody;

        [Header("Conditions")]
        
        private bool Dead = false;
        
        [Header("Attack")]
        public bool WantAttack;
        public bool CanAttack;

        [Header("Object")] 
        
        public List<GameObject> Object;

        private RoundHundler _roundHundler;
        private BonusToZombie _bonusToZombie;

        [SerializeField] private Player _player;

        public float Health
        {
            get => CurrentHealth;
            set
            {
                CurrentHealth = value;
                if (CurrentHealth <= 0 && !Dead)
                {
                    Dead = true;
                    Die();
                }
            }
        }

        [ContextMenu("TakeDamge (Test Methode)")]
        public void TakeDamaga()
        {
            Health -= 50;
        }

        public void Damaged(float damage, Player player)
        {
            Health -= damage;
            _player = player;
        }
        
        private void Awake()
        {
            Targets = GameObject.FindGameObjectsWithTag(TargetTag).ToList();  
            int index = Random.Range(0, Targets.Count);

            if (Targets.Count != 0)
            {
                Target = Targets[index];
            }
            
            NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            
            _roundHundler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RoundHundler>();
            _bonusToZombie =  GameObject.FindGameObjectWithTag("GameManager").GetComponent<BonusToZombie>();
        }

        private void Start()
        {
            
            
            SetData();
            SetRoundBonusStat();
            gameObject.GetComponent<SphereCollider>().radius = TypeZombie.AttackRange;
        }
        public void SetData()
        {
            Damage = TypeZombie.Damage;
            Speed = TypeZombie.Speed;
            Acceleration = TypeZombie.Acceleration;
            MaxHealth = TypeZombie.MaxHealth;
            CurrentHealth = MaxHealth;
            AttackSpeed = TypeZombie.AttackSpeed;
            AttackRange = TypeZombie.AttackRange;
            NavMeshAgent.acceleration = Acceleration;
            NavMeshAgent.speed = Speed;
            Price = TypeZombie.PriceZombie;
        }
        
        public void SetRoundBonusStat()
        {
            MaxHealth *= _bonusToZombie.Bonus[_roundHundler.Round.CurrentRound];
            CurrentHealth = MaxHealth;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == Target.gameObject)
            {
                Debug.Log("Touche la cible");
                CanAttack = true;
            }
        }

        public void Die()
        {
            GameObject.FindWithTag("GameManager").GetComponent<RoundHundler>().ZombieToKillCount++;
            if (_player)
            {
                _player.GetComponent<PlayerScore>().ZombieKilled++;
            }
            Debug.Log("Die");
            Destroy(gameObject);
        }
    }
}
