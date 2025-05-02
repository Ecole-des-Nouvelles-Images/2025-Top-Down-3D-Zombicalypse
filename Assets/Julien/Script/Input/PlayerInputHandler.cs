using System;
using Julien.Script;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private bool _isControllerConnected;
        public static event Action<bool> OnInputDeviceChanged;
        
        [SerializeField] private bool Holding;
        private Player _player;
        private InventoryPlayer _inventory;
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _player = GetComponent<Player>();
            _inventory = GetComponent<InventoryPlayer>();
        }

        private void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
            
            _playerInput.actions["Fire"].performed += OnFire;
            _playerInput.actions["Fire"].canceled += OnFire;
            
            _playerInput.actions["SwitchWeapon"].performed += OnSwitchWeapon;

            _playerInput.actions["Interact"].started += OnInteract;
            
            _playerInput.actions["Drop"].performed += OnDrop;
            
            _playerInput.actions["Reload"].performed += OnReload;
            
            _playerInput.actions["Inventory"].canceled += OnOpenInventory;
            
            _playerInput.actions["Move"].performed += OnMove;
            _playerInput.actions["Move"].canceled += OnMove;

            _playerInput.actions["Aim"].performed += OnAim;
            _playerInput.actions["Aim"].canceled += OnAim;

            // GameManager.PlayerPrefabs.Add(PlayerInputManager.playerPrefab.gameObject);
            // Debug.Log(PlayerInputManager.playerPrefab.gameObject+ " Join the game " );
        }
        
        private void OnDisable()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;
            
            _playerInput.actions["Fire"].performed -= OnFire;
            _playerInput.actions["Fire"].canceled -= OnFire;
            
            _playerInput.actions["SwitchWeapon"].performed -= OnSwitchWeapon;
            
            _playerInput.actions["Interact"].started -= OnInteract;
            
            _playerInput.actions["Drop"].performed -= OnDrop;
            
            _playerInput.actions["Reload"].performed -= OnReload;
            
            _playerInput.actions["Inventory"].canceled -= OnOpenInventory;
            
            _playerInput.actions["Move"].performed -= OnMove;
            _playerInput.actions["Move"].canceled -= OnMove;
            
            _playerInput.actions["Aim"].performed -= OnAim;
            _playerInput.actions["Aim"].canceled -= OnAim;
        }
    
        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
            { 
                DetectCurrentInputDevice();
            }
        }
        
        private void DetectCurrentInputDevice()
        {
            _isControllerConnected = Gamepad.all.Count > 0;
            OnInputDeviceChanged?.Invoke(_isControllerConnected);
            
            //Debug.Log(_isControllerConnected
            //? "Controller connected: Switching to Gamepad controls."
            //: "No controller connected: Switching to Keyboard/Mouse controls.");
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            _player.Fire(context.ReadValueAsButton());
        }

        private void OnSwitchWeapon(InputAction.CallbackContext context)
        {
            _inventory.SwitchWeapon();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _player.SetParameter(context.ReadValue<Vector2>());
        }

        private void OnAim(InputAction.CallbackContext context)
        {
            _player.Aim(context.ReadValue<Vector2>());
        }

        private void OnReload(InputAction.CallbackContext context)
        {
            _inventory.Reload();
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            _player.Interact();
        }

        private void OnDrop(InputAction.CallbackContext context)
        {
            _inventory.DropWeapon();
        }
        
        private void OnOpenInventory(InputAction.CallbackContext context)
        {
            _player.OpenInventory();
        }
    }
}
