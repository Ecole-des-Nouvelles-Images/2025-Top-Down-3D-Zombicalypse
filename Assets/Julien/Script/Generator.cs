using System;
using System.Collections.Generic;
using System.Linq;
using Julien.Script;
using UnityEngine;

namespace Script
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] private float _maxTimeBefforDamaged;
        [SerializeField] private float _timeBefforDamaged;
        [SerializeField] private float _maxTimeDamage;
        [SerializeField] private float _timedamage;
        
        
        [SerializeField] private float _maxTimer;
        [SerializeField] private float _timer;
        
        public bool IsBreak;
        [SerializeField] private bool _isDestroyed;

        public float MaxHealth;
        [SerializeField] private float _health;

        public event Action<float> OntakeDamage; 
        
        
        public float Health
        {
            get => _health;
            set
            {
                _health = value;
                if (_health <= 0)
                {
                    EndGame();
                }
            }
        }

        private void Update()
        {
            if (IsBreak)
            {
                On();
            }
            else 
            { 
                Off();
            }
            
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            OntakeDamage?.Invoke(Health);
        }

        public void On()
        {
            _timeBefforDamaged = _maxTimeBefforDamaged;
            _timedamage = _maxTimeDamage;
            
            _timer -= Time.deltaTime;
            if (_timer <= 0 )
            {
                _timer = _maxTimer; 
            }
        }

        public void Off()
        {
            _timeBefforDamaged -= Time.deltaTime;
            
            if (_timeBefforDamaged <= 0)
            {
                _timedamage -= Time.deltaTime;
                if (_timedamage <= 0)
                {
                    Health -= 1;
                    _timedamage = _maxTimeDamage;
                }
            }
        }

        [ContextMenu("TakeDamage debug")]
        public void TakeDamageDebug()
        {
            Health -= 20;
            OntakeDamage?.Invoke(Health);
        }

        [ContextMenu("EndGame")]
        public void EndGame()
        {
            GameObject.FindWithTag("GameManager").GetComponent<EndingGame>().EndGame();
            Debug.Log("EndGame");
        }
    }
}
