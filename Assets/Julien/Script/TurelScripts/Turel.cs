using System.Collections.Generic;
using System.Linq;
using Julien.Script.Struc;
using Julien.Script.TurelScripts.State;
using Script.Data.TurellData;
using Script.Turel.State;
using UnityEngine;

namespace Julien.Script.TurelScripts
{
    public class Turel : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        public TurelData TurelType;
        public TurelState CurrentStat;

        [Header("Data")] 
        
        public TurelWrap TurelWrap;
        
        // public int CurrentAmmo;
        // public float Damage;
        // public float MaxFireRate;
        // public float FireRate;
        // public float Precision;
        // public float Range;
        // public float BulletSpeed;
        // public float BulletRange;
        // [SerializeField] private float _health;
        //public GameObject AmmoType;

        [Header("IA")] 
        
        public GameObject TurelRenderer;
        public GameObject SpawnBullet;
        public GameObject AimTarget;
        public List<GameObject> Targets = new List<GameObject>();
        public GameObject Target;

        [Header("Conditions")] 
        
        public bool CanSetDown;
        public bool CanFire;
        public bool HaveTarget;
        public float Health
        {
            get => TurelWrap.Health;
            
            set
            {
                TurelWrap.Health = value;
                if (TurelWrap.Health <= 0)
                {
                    Die();
                }
            }
        }
        
        [SerializeField] private SphereCollider _sphereTriggerCollider;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
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
        public void SetInfo()
        {
            _sphereTriggerCollider = gameObject.GetComponent<SphereCollider>();
            _sphereTriggerCollider.radius = TurelWrap.Range;
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

        public void SetParameters(TurelWrap turelWrap)
        {
            TurelWrap = turelWrap;
        }
    }
}
