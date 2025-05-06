using System.Collections;
using System.Collections.Generic;
using Julien.Script.Interface;
using Julien.Script.Static;
using Script.Data.PlayerData;
using Script.Input;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Julien.Script
{
    public class Player : MonoBehaviour
    {
        [Header("-- Input Template ----------------------------------------------------------------------")]
        
        public List<MonoBehaviour> Inputhandlers = new List<MonoBehaviour>();
            
        public int PlayerIndex;
        
        [Header("Player Data")]
        
        public PlayerData PlayerData;

        [Header("Player Stat")] 
        
        [SerializeField] private float _health;
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

        [Header("References")] 
        
        [SerializeField] private GameObject _gameManager;
        public GameObject PlayerRenderer;
        private Vector2 _move;
        private InventoryPlayer _inventory;

        [SerializeField] private List<GameObject> InteractsGameObject;
        
        
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
            transform.position = GameManagerStatic.positions[PlayerIndex];
        }

        private void Start()
        {
            _inventory = gameObject.GetComponent<InventoryPlayer>();
            DisplayData();
            handingObject.SwitchWeapon();
            _inventory.AutomaticSwitch();
            _gameManager = GameObject.FindWithTag("GameManager");
            gameObject.GetComponent<PlayerInputHandlerTurel>().enabled = true;
            SwitchInputHandler(0);
        }

        private void DisplayData()
        {
            Health = PlayerData.health;
            Speed = PlayerData.Speed;
        }

        private void Update()
        {
            OnMove(_move);
            if (_isHolding)
            {
                Fire(_isHolding);
            }
            if (_inventory.UpgraderWrap.UpgraderType)
            {
                RaycastHit hit;
                
                Debug.DrawRay(PlayerRenderer.transform.position, PlayerRenderer.transform.forward * 2, Color.blue, 1f);
                if (Physics.Raycast(PlayerRenderer.transform.position, PlayerRenderer.transform.forward, out hit, 2))
                {
                    if (hit.collider.CompareTag("Turel"))
                    {
                        currentAimTurel = hit.transform.gameObject;
                        Debug.Log("Touche une tourelle");
                    }
                    else
                    {
                        currentAimTurel = null;
                        Debug.Log("Touche rien");
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
               _aimTarget.transform.position = new Vector3(gameObject.transform.position.x + oldValue.x, gameObject.transform.position.y + 1, gameObject.transform.position.z + oldValue.y);
            }
        }
        
        // Tirer
        public void Fire(bool readValueAsButton)
        {
            _isHolding = readValueAsButton;
            if (readValueAsButton && _canShoot && !isReloading && _inventory.equipedWeaponWrap.CurrentAmmo - _inventory.equipedWeaponWrap.Weapon.RemoveAmmoParFire !>= 0)
            {
                _inventory.equipedWeaponWrap.Weapon.Fire(_spawnBullet.transform);
                StartCoroutine("ShootDelay", _inventory.equipedWeaponWrap.Weapon.FireRate);
                _canShoot = false;
                _inventory.equipedWeaponWrap.CurrentAmmo -= _inventory.equipedWeaponWrap.Weapon.RemoveAmmoParFire;
            }
        }
        
        public IEnumerator ShootDelay(float timer)
        {
            yield return new WaitForSeconds(timer);
            _canShoot = true;
        }
        
        
        // recharger
        

       
        
        public void Die()
        {
            Debug.Log("Meurt");
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
            if (other.gameObject.CompareTag("Interactable"))
            {
                InteractsGameObject.Add(other.gameObject);
                InteractsGameObject[0].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Interactable"))
            {
                other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                InteractsGameObject.Remove(other.gameObject);
            }
        }
    }
}
