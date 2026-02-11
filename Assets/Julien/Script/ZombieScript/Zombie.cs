using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Julien.Script.Data.Zombie;
using Julien.Script.PlayerScripts;
using Julien.Script.Static;
using Julien.Script.TurelScripts;
using Julien.Script.ZombieScript.States;
using Script;
using UnityEngine;
using UnityEngine.AI;

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
        [SerializeField] private float _chanceDropAmmo;

        [Header("Target")] 
        
        public List<GameObject> Targets;
        public GameObject Target;
        public string TargetTag;

        public ZombieState CurrentState;

        public NavMeshAgent NavMeshAgent;
        public Rigidbody Rigidbody;

        [Header("Conditions")]
        
        public bool Explose;
        public bool Dead;
        
        [Header("Attack")]
        public bool WantAttack;
        public bool CanAttack;

        [Header("Object")] 
        
        public List<GameObject> Object;

        private RoundHundler _roundHundler;
        private BonusToZombie _bonusToZombie;
        [SerializeField] private List<GameObject> _prefabs;
        [SerializeField] private float _spawnRadius;

        [SerializeField] private Player _player;

        [Header("animator")]

        public Animator Animator;

        [Header("VisualEffect")] 
        
        public ParticleSystem BloodEffect;

        public float Health
        {
            get => CurrentHealth;
            set
            {
                CurrentHealth = value;
                if (CurrentHealth <= 0 && !Dead)
                {
                    Dead = true;

                    float rand = Random.Range(0, 100);
                    if (rand <= _spawnRadius)
                    {
                        SpawnPrefab();
                        Debug.Log("Spawn bullet");
                    }
                    
                    GameManagerStatic.lastZombiePosition = gameObject.transform.position;
                    Die(true);
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
            _player = player;
            Health -= damage;
            BloodEffect.Play();
            Animator.SetTrigger("Hit");
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

        public void PlayScream()
        {
            float rand = Random.Range(0, 5000);
            if (rand <= 1)
            {
                SoundManager.Instance.PlaySound(gameObject, SoundManager.Instance.ScreamZombie, 0.2f);
            }
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

        public void Die(bool deadByPlayer)
        {
            Animator.SetBool("Die", true);
            
            GameObject.FindWithTag("GameManager").GetComponent<RoundHundler>().ZombieToKillCount++;
            if (_player)
            {
                _player.GetComponent<PlayerScore>().ZombieKilled++;
            }
            if (deadByPlayer)
            {
                StaticAction.OnAddPoint.Invoke(TypeZombie.Point);
            }
            
            SoundManager.Instance.PlaySound(SoundManager.Instance.gameObject, SoundManager.Instance.DeadZombie, 0.3f);
            StartCoroutine("DestroyZombieDelay");
        }

        public IEnumerator DestroyZombieDelay()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }

        public void SpawnPrefab()
        {
            int randInt = Random.Range(0, _prefabs.Count);
            GameObject prefab = _prefabs[randInt];
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
