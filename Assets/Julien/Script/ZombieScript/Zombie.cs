using System.Collections.Generic;
using System.Linq;
using Julien.Script;
using Julien.Script.Data.Zombie;
using Script.ZombieScript.States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Script.ZombieScript
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
        
        [Header("Attack")]
        public bool WantAttack;
        public bool CanAttack;

        [Header("Object")] 
        
        public List<GameObject> Object;

        private RoundHundler _roundHundler;
        private BonusToZombie _bonusToZombie;

        public float Health
        {
            get => CurrentHealth;
            set
            {
                CurrentHealth = value;
                if (CurrentHealth <= 0 )
                {
                    Die();
                }
            }
        }

        [ContextMenu("TakeDamge (Test Methode)")]
        public void TakeDamaga()
        {
            Health -= 50;
        }

        public void Damaged(float damage)
        {
            Health -= damage;
        }
        
        private void Awake()
        {
            Targets = GameObject.FindGameObjectsWithTag(TargetTag).ToList();
            int index = Random.Range(0, Targets.Count);
            Target = Targets[index];
            
            NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            
            _roundHundler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RoundHundler>();
            _bonusToZombie =  GameObject.FindGameObjectWithTag("GameManager").GetComponent<BonusToZombie>();
        }

        private void Start()
        {
            SetData();
            SetRoundBonusStat();
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
            MaxHealth *= _bonusToZombie.Bonus[_roundHundler.CurrentWave];
            CurrentHealth = MaxHealth;
        }

        public void Die()
        {
            GameObject.FindWithTag("GameManager").GetComponent<RoundHundler>().ZombieToKillCount++;
            Destroy(gameObject);
        }
    }
}
