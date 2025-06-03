using Julien.Script.Interface;
using Julien.Script.PlayerScripts;
using Julien.Script.Static;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Julien.Script
{
    public class Generator : MonoBehaviour, ITakeDamage, IInteractable
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

        [SerializeField] private float _maxRepar;
        [SerializeField] private float _currentRepar;
        [SerializeField] private float _reparParClick;
        [SerializeField] private float _reparHealthParClick;

        [Header("Reférence")]
        private GameObject _gameManager;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private GameObject _reparBarParent;
        [SerializeField] private Image _reparBar;
        [SerializeField] private Image _TimeBeforDamageBar;
        
        [Header("VisualEffect")] 
        
        [SerializeField] private GameObject _brokenEffect;
        

        private void Start()
        {
            _gameManager = GameObject.Find("GameManager");
            _audioSource.clip = SoundManager.Instance.GeneratorWork;
            _audioSource.Play();
        }

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
            if (!IsBreak)
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
            _brokenEffect.SetActive(false);
            _timeBefforDamaged = _maxTimeBefforDamaged;
            _timedamage = _maxTimeDamage;
            _reparBarParent.SetActive(false);
            _timer -= Time.deltaTime;
            if (_timer <= 0 )
            {
                float rand = Random.Range(0f, 100f);
                if (rand <= 1f)
                {
                    IsBreak = true;
                }
                _timer = _maxTimer; 
            }
        }

        public void Off()
        {
            _brokenEffect.SetActive(true);
            _timeBefforDamaged -= Time.deltaTime;
            _TimeBeforDamageBar.fillAmount = _timeBefforDamaged / _maxTimeBefforDamaged;
            _reparBarParent.SetActive(true);
            if (_timeBefforDamaged <= 0)
            {
                _timedamage -= Time.deltaTime;
                if (_timedamage <= 0)
                {
                    Health -= 1;
                    _timedamage = _maxTimeDamage;
                    StaticAction.OntakedDamage.Invoke(Health, MaxHealth);
                }
            }
        }

        [ContextMenu("TakeDamage debug")]
        public void TakeDamageDebug()
        {
            Health -= 20;
            StaticAction.OntakedDamage?.Invoke(Health, MaxHealth);
        }

        [ContextMenu("EndGame")]
        public void EndGame()
        {
            GameObject.FindWithTag("GameManager").GetComponent<EndingGame>().EndGame();
            Debug.Log("EndGame");
        }

        public void takeDamage(float damage)
        {
            Health -= damage;
            StaticAction.OntakedDamage?.Invoke(Health, MaxHealth);
        }

        public void Activate(Player player)
        {
            if (IsBreak)
            {
                SoundManager.Instance.PlaySound(gameObject, SoundManager.Instance.GeneratorRepart);
                _currentRepar += _reparParClick;
                SetReparBar();
                if (_currentRepar >= _maxRepar)
                {
                    IsBreak = false;
                    _currentRepar = 0;
                    _reparBar.fillAmount = 0;
                }
            }

            if (_gameManager.GetComponent<RoundHundler>().InBreak && !IsBreak)
            {
                Health += _reparHealthParClick;
                Health = Mathf.Clamp(Health, 0, MaxHealth);
                StaticAction.OntakedDamage?.Invoke(Health, MaxHealth);
            }
        }

        public void SetReparBar()
        {
            _reparBar.fillAmount = _currentRepar / _maxRepar;
        }
    }
}
