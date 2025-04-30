using System.Collections.Generic;
using System.Linq;
using Script.Data.TurellData;
using Script.Turel.State;
using UnityEngine;

namespace Script.Turel
{
    public class Turel : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        public TurelData TurelType;
        public TurelState CurrentStat;

        [Header("Data")]
        
        public int CurrentAmmo;
        public float Damage;
        public float MaxFireRate;
        public float FireRate;
        public float Precision;
        public float Range;
        public float BulletSpeed;
        public float BulletRange;
        [SerializeField] private float _health;
        public GameObject AmmoType;

        [Header("IA")] 
        
        public GameObject TurelRenderer;
        public GameObject SpawnBullet;
        public GameObject AimTarget;
        public List<GameObject> Targets = new List<GameObject>();
        public GameObject Target;
        
        [Header("Conditions")]
        
        public bool CanFire;
        public bool HaveTarget;
        public float Health
        {
            get => _health;
            
            set
            {
                _health = value;
                if (_health <= 0)
                {
                    Die();
                }
            }
        }
        
        [SerializeField] private SphereCollider _sphereTriggerCollider;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            SetData();
            SetInfo();
            CurrentStat = new TurelShearch();
        }

        private void FixedUpdate()
        {
            foreach (GameObject target in Targets.ToList())
            {
                if (!target)
                {
                    Targets.Remove(target);
                }
            }
            if (!Target) Target = null;
            if (Targets.Count > 0) Target = Targets[0];
            HaveTarget = Target;
        }

        public void SetData()
        {
            CurrentAmmo = TurelType.MaxAmmo;
            Damage = TurelType.Damage;
            MaxFireRate = TurelType.FireRate;
            FireRate = TurelType.FireRate;
            Precision = TurelType.Precision;
            Range = TurelType.Range;
            Health = TurelType.MaxHealth; 
            AmmoType = TurelType.AmmoType;
            BulletSpeed = TurelType.BulletSpeed;
            BulletRange = TurelType.BulletRange;
        }
        public void SetInfo()
        {
            _sphereTriggerCollider = gameObject.GetComponent<SphereCollider>();
            _sphereTriggerCollider.radius = Range;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Zombie"))
            {
                Debug.Log("add zombie");
                Targets.Add(other.gameObject);
                Target = Targets[0];
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Zombie"))
            {
                Target = null;
                Targets.Remove(other.gameObject);
                if (Targets.Count > 0)
                {
                    Target = Targets[0];
                }
            }
        }
        
        [ContextMenu("Take damage debug")]
        public void TakeDamageDebug()
        {
            Health -= 20;
        }
        
        public void Die()
        {
            Destroy(gameObject);
        }
    }
}
