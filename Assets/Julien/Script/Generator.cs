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
        
        [SerializeField] private float _healthHH;
        
        public float Health
        {
            get => _healthHH;
            set
            {
                _healthHH = value;
                if (_healthHH <= 0)
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

        [ContextMenu("EndGame")]
        public void EndGame()
        {
            GameObject.FindWithTag("GameManager").GetComponent<EndingGame>().EndGame();
            Debug.Log("EndGame");
        }
    }
}
