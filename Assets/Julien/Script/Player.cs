using System.Collections;
using System.Collections.Generic;
using Julien.Script.Struc;
using Julien.Script.TurelScripts;
using Script;
using Script.Data.PlayerData;
using Script.Input;
using UnityEngine;
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
        [SerializeField] private bool _isReloading;
        [SerializeField] private bool _canShoot;
        
        [Header("Movement")] 
        
        [SerializeField] private GameObject _aimTarget;
        [SerializeField] private Rigidbody _rigidbody;

        [FormerlySerializedAs("_weaponHanding")]
        [Header("Other")] 
        
        public HandingObject handingObject;
        [SerializeField] private GameObject _dropPrefab;

        [SerializeField] private GameObject _currentAimTurel;

        [Header("References")] 
        
        [SerializeField] private GameObject _gameManager;
        [SerializeField] private GameObject _playerRenderer;
        private Vector2 _move;
        private InventoryPlayer _inventory;
        private IInteractable _interactable;
        
        private void Awake()
        {
           // _rigidbody = GetComponent<Rigidbody>();
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

            if (_inventory.UpgraderWrap.Upgrader)
            {
                RaycastHit hit;
                
                Debug.DrawRay(_playerRenderer.transform.position, _playerRenderer.transform.forward * 2, Color.green, 1f);
                if (Physics.Raycast(_playerRenderer.transform.position, _playerRenderer.transform.forward, out hit, 10))
                {
                    if (hit.transform.CompareTag("Turel"))
                    {
                        _currentAimTurel = hit.transform.gameObject;
                        Debug.Log("Touche une tourelle");
                    }
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
            if (readValueAsButton && _canShoot && !_isReloading && _inventory.equipedWeaponWrap.CurrentAmmo - _inventory.equipedWeaponWrap.Weapon.RemoveAmmoParFire !>= 0)
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
        public void Reload()
        {
            if (_inventory.equipedWeaponWrap.CurrentMagazin - 1 !>= 0 && _inventory.equipedWeaponWrap.CurrentAmmo != _inventory.equipedWeaponWrap.Weapon.MaxAmmo && !_isReloading)
            {
                StartCoroutine("ReloadDelay", _inventory.equipedWeaponWrap.Weapon.ReloadTime);
            }
        }

        private IEnumerator ReloadDelay(float timer)
        {
            _isReloading = true;
            yield return new WaitForSeconds(timer);
            _isReloading = false;
            _inventory.equipedWeaponWrap.CurrentAmmo = _inventory.equipedWeaponWrap.Weapon.MaxAmmo;
            _inventory.equipedWeaponWrap.CurrentMagazin--;
        }
        
        // changer d'arme
        public void SwitchWeapon()
        {
            _inventory.Switch();
            StopCoroutine("ReloadDelay");
            handingObject.SwitchWeapon();
            _isReloading = false;
        }
        
        public void Die()
        {
            Debug.Log("Meurt");
        }
        
        public void DropWeapon()
        {
            _inventory.DropWeapon(_dropPrefab);
        }

        public void DropTurel()
        {
            _inventory.SetDownTurel();
        }
        
        // Interagir
        
        public void Interact()
        {
            // regarder si _interactable n'est pas vide
            if (_interactable != null)
            {
                _interactable.Activate(this);
            }
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

        public void PutBonus()
        {
            Debug.Log("PutBonus");

            if (_currentAimTurel)
            {
                _currentAimTurel.GetComponent<Turel>().TurelWrap.AddBonus(_inventory.UpgraderWrap);
                _inventory.UpgraderWrap = new UpgraderWrap();
                Destroy(handingObject.HandingBonus.transform.GetChild(0).gameObject);
                handingObject.HandingWeapon.SetActive(true);
                SwitchInputHandler(0);
            }
        }
        
        public void SwitchInputHandler(int index)
        {
            foreach (MonoBehaviour component in Inputhandlers)
            {
                component.enabled = false;
            } 
            Inputhandlers[index].enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            // If collide with an object with tag "interactable"
            if (other.gameObject.CompareTag("Interactable"))
            {
                _interactable = other.GetComponent<IInteractable>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Interactable"))
            {
                _interactable = null;
            }
        }
    }
}
