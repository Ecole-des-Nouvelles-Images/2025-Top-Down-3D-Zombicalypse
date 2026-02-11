using System.Collections.Generic;
using System.Linq;
using Julien.Script.Data.Turel;
using Julien.Script.Interface;
using Julien.Script.Struc;
using Julien.Script.TurelScripts.State;
using Julien.Script.ZombieScript;
using Script.Turel.State;
using UnityEngine;
using UnityEngine.UI;

namespace Julien.Script.TurelScripts
{
    public class Turel : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameObject _AimCollider;
        
        public TurelData TurelType;
        public TurelState CurrentStat;

        [SerializeField] private Image _imageHealth;

        [Header("Data")] 
        
        public TurelWrap TurelWrap;

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
            TurelWrap.SetFirstData();
            CurrentStat = new TurelShearch();
        }

        private void FixedUpdate()
        {
            foreach (GameObject target in Targets.ToList())
            {
                if (target.GetComponent<Zombie>().Dead)
                {
                    Target = null;
                    Targets.Remove(target);
                }
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
        }
        
        [ContextMenu("Take damage debug")]
        public void TakeDamageDebug()
        {
            Health -= 20;
        }
        
        public void Die()
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.gameObject, SoundManager.Instance.TurelDestroy, 0.5f);
            Destroy(gameObject);
        }

        public void SetParameters(TurelWrap turelWrap)
        {
            TurelWrap = turelWrap;
            _AimCollider.gameObject.transform.localScale = new Vector3(turelWrap.Range, 1, turelWrap.Distance);
        }
        
        public void UpdateTurel(UpgraderWrap turelWrap)
        {
            _AimCollider.transform.localScale += new Vector3(turelWrap.AddRange, 0, turelWrap.AddDistance);
            TurelWrap.AddBonus(turelWrap);
        }

        public void takeDamage(float damage)
        {
            Health -= damage;
            _imageHealth.fillAmount = Health / TurelWrap.MaxHEalth;
        }

        public void VisualEffect()
        {
            Transform bulletParticle = gameObject.transform.Find("BulletFallVfx");
            bulletParticle.gameObject.GetComponent<ParticleSystem>().Play();
                
            Transform flashParticle = gameObject.transform.Find("FlashTurretVfx");
            flashParticle.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
}
