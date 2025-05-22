using System.Collections;
using System.Collections.Generic;
using Julien.Script.HUD;
using Julien.Script.Interface;
using Julien.Script.Static;
using Script.Data.PlayerData;
using Script.Input;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Julien.Script.PlayerScripts
{
    public class Player : MonoBehaviour, ITakeDamage
    {
        [Header("-- Input Template ----------------------------------------------------------------------")]
        
        public List<MonoBehaviour> Inputhandlers = new List<MonoBehaviour>();
            
        public int PlayerIndex;
        
        [Header("Player Data")]
        
        public PlayerData PlayerData;

        [Header("Player Stat")]

        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;
        public float timeBeforRespawn;
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
        
        
        public float Speed;

        [Header("-- Private --------------------------------------------------------------------")]
        
        [SerializeField] private GameObject _spawnBullet;
        [SerializeField] private bool _isHolding;
        [SerializeField] public bool isReloading;
        [SerializeField] private bool _canShoot;
        
        [Header("Movement")] 
        
        [SerializeField] private GameObject _aimTarget;
        [SerializeField] private Rigidbody _rigidbody;

        [FormerlySerializedAs("_weaponHanding")]
        [Header("Other")] 
        
        public HandingObject handingObject;
        [SerializeField] private GameObject _dropPrefab;
        
        [SerializeField] public GameObject currentAimTurel;

        [Header("References HUD")] 
        [SerializeField] private GameObject _hudScore;
        [SerializeField] private HUDPlayerScoreEnd _hudPlayerScoreEnd;
        [SerializeField] private GameObject HudPrefab;
        public HUDPlayer hudPlayer;
        [SerializeField] private GameObject _hudDeadPlayer;
        private GameObject HudParent;
        
        [Header("References Script")]
        private GameObject _gameManager;
        public GameObject PlayerRenderer;
        public List<GameObject> Cloths = new List<GameObject>();
        private Vector2 _move;
        [FormerlySerializedAs("_inventory")] public InventoryPlayer Inventory;
        private PlayerScore _playerScore;
        
        [Header("animator")]
        [SerializeField] private Animator _animator;

        [SerializeField] private float _verticalValue;
        [SerializeField] private float _horizontalValue;

        [SerializeField] private GameObject _spineBone;
        [SerializeField] private List<GameObject> InteractsGameObject;

        private Vector3 _playerUpdateDir;
         
        
        private void Awake()
        {
            // ne pas detruire le gameobject lors d'un chargement de scene
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneloaded;
        }
        
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneloaded;
        }
        
        private void OnSceneloaded(Scene arg0, LoadSceneMode arg1)
        {
            // instancier au chargement de la scene un prefab de son HUD ( inventaire )
            int index = PlayerIndex - 1;
            
            HudParent = GameObject.FindWithTag("HudMultiplayer");
            GameObject hud = Instantiate(HudPrefab, HudParent.transform.position, quaternion.identity, HudParent.transform);
            hudPlayer = hud.GetComponent<HUDPlayer>();
            hudPlayer.SetInfoOnStart(this);

            
            // HUD SCORE
            GameObject hudScore = Instantiate(_hudScore, HudParent.transform.position, quaternion.identity, HudParent.transform);
            _hudPlayerScoreEnd = hudScore.GetComponent<HUDPlayerScoreEnd>();
            _hudPlayerScoreEnd.SetOnStart(this);
            
            
            GameManagerStatic.Players.Add(gameObject);
            // changer la place du joueur au début au chargement de la scene
            transform.position = GameManagerStatic.positions[index];
            
            Debug.Log("rhnbvihechge,rchnfkljndrg");
        }

        private void Start()
        {
            _playerScore = GetComponent<PlayerScore>();
            Inventory = gameObject.GetComponent<InventoryPlayer>();
            DisplayData();
            handingObject.SwitchWeapon();
            Inventory.AutomaticSwitch();
            _gameManager = GameObject.FindWithTag("GameManager");
            gameObject.GetComponent<PlayerInputHandlerTurel>().enabled = true;
            SwitchInputHandler(0);
        }

        private void DisplayData()
        {
            _maxHealth = PlayerData.health;
            Health = _maxHealth;
            Speed = PlayerData.Speed;
        }

        private void Update()
        {
            // animation des jambes
            _verticalValue = _rigidbody.linearVelocity.z;
            _horizontalValue = _rigidbody.linearVelocity.x;
            
            _animator.SetFloat("Horizontal", _horizontalValue);
            _animator.SetFloat("Vertical", _verticalValue);
            
            OnMove(_move);
            if (_isHolding)
            {
                Fire(_isHolding);
            }
            if (Inventory.UpgraderWrap.UpgraderType)
            {
                RaycastHit hit;
                Debug.Log(_aimTarget.transform.position);
                Debug.DrawRay(_spineBone.transform.position, _aimTarget.transform.localPosition * 2 - new Vector3(0,2f,0), Color.blue, 1f);
                if (Physics.Raycast(_spineBone.transform.position, _aimTarget.transform.localPosition * 2 - new Vector3(0,2f,0), out hit, 2))
                {
                    if (hit.collider.CompareTag("Turel"))
                    {
                        currentAimTurel = hit.transform.gameObject;
                    }
                    else
                    {
                        currentAimTurel = null;
                    }
                }
                else
                {
                    currentAimTurel = null;
                }
            }

            for (int i = 0; i < InteractsGameObject.Count; i++)
            {
                if (!InteractsGameObject[i])
                {
                    InteractsGameObject.Remove(InteractsGameObject[i]);
                }
            }
        }
        
        // Set parameter récupere la value du joystick, et le set à la variable move.
        // Pour ensuite appeler On move avec _move comme paramettre.
        public void SetParameter(Vector2 moveValue)
        {
            _move = moveValue;
        }
        public void OnMove(Vector2 moveValue)
        {
            float horizontal = moveValue.x;
            float vertical = moveValue.y;

            Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
            _rigidbody.linearVelocity = moveDirection * Speed;
        }
        
        // Viser
        public void Aim(Vector2 valueAim)
        {
            // stacker la valeur que si elle est superieur à 0.8f
            if (Mathf.Abs(valueAim.x) >= 0.5f || Mathf.Abs(valueAim.y) >= 0.5f)
            {
                Vector2 oldValue = valueAim;
                _playerUpdateDir = new Vector3(valueAim.x, 0, valueAim.y);
               _aimTarget.transform.position = new Vector3(gameObject.transform.position.x + oldValue.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z + oldValue.y);
            }

            //Debug.Log(valueAim);
        }
        
        // Tirer
        public void Fire(bool readValueAsButton)
        {
            _isHolding = readValueAsButton;
            if (readValueAsButton && _canShoot && !isReloading && Inventory.equipedWeaponWrap.CurrentAmmo - Inventory.equipedWeaponWrap.Weapon.RemoveAmmoParFire !>= 0)
            {
                Inventory.equipedWeaponWrap.Weapon.Fire(_spawnBullet.transform, this, _playerUpdateDir);
                StartCoroutine("ShootDelay", Inventory.equipedWeaponWrap.Weapon.FireRate);
                _canShoot = false;
                Inventory.equipedWeaponWrap.CurrentAmmo -= Inventory.equipedWeaponWrap.Weapon.RemoveAmmoParFire;
                if (hudPlayer) hudPlayer.SetHudInfo();
            }
        }
        
        public IEnumerator ShootDelay(float timer)
        {
            yield return new WaitForSeconds(timer);
            _canShoot = true;
        }

        [ContextMenu("Debug take damage")]
        public void takeDamageDebug()
        {
            takeDamage(20);
        }
        
        [ContextMenu("Die")]
        public void Die()
        {
            Debug.Log("Meurt");
            _playerScore.DieCount++;
            GameObject hudDead = Instantiate(_hudDeadPlayer, HudParent.transform);
            GameManagerStatic.Players.Remove(gameObject);
            hudDead.GetComponent<HUDDeadPlayer>().SetInfo(this);
        }

        public void Respawn()
        {
            timeBeforRespawn += 5;
            Health = _maxHealth;
            hudPlayer.HUDPlayerHealth.SetHealthBarHUD(Health, _maxHealth);
            GameManagerStatic.Players.Add(gameObject);
            _canShoot = true;
        }
       
        public void OpenInventory()
        {
            Debug.Log("Open Inventory");
            Debug.Log(PlayerIndex);
        }

        public void RotateTurel(float rotateValue)
        {
            Debug.Log(rotateValue);
            GameObject turelHologram = handingObject.HandingTurel.transform.GetChild(0).gameObject;
            turelHologram.transform.Rotate(Vector3.up * (rotateValue * 150 * Time.deltaTime));
        }
        
        
        public void SwitchInputHandler(int index)
        {
            foreach (MonoBehaviour component in Inputhandlers)
            {
                component.enabled = false;
            } 
            Inputhandlers[index].enabled = true;
        }
        
        
        // Interagir
        public void Interact()
        {
            if (InteractsGameObject[0] != null)
            {
                InteractsGameObject[0].GetComponent<IInteractable>().Activate(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<IInteractable>() != null)
            {
                InteractsGameObject.Add(other.gameObject);
                if ( InteractsGameObject[0].gameObject.transform.Find("CanvaInteract"))
                {
                    InteractsGameObject[0].gameObject.transform.Find("CanvaInteract").gameObject.SetActive(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<IInteractable>() != null)
            {
                if (other.gameObject.transform.transform.Find("CanvaInteract"))
                {
                    other.gameObject.transform.transform.Find("CanvaInteract").gameObject.SetActive(false);
                }
                InteractsGameObject.Remove(other.gameObject);
            }
        }

        public void takeDamage(float damage)
        {
            Health -= damage;
            hudPlayer.HUDPlayerHealth.SetHealthBarHUD(Health, _maxHealth);
        }
    }
}
