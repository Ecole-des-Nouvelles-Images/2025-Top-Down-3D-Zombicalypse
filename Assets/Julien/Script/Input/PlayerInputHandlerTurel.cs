using System;
using Julien.Script;
using Julien.Script.PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Input
{
    public class PlayerInputHandlerTurel : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private bool _isControllerConnected;
        public static event Action<bool> OnInputDeviceChanged;
        
        private Player _player;
        private InventoryPlayer _inventory;

        private bool _isRotating;
       [SerializeField] private float RotateValue;
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _player = GetComponent<Player>();
            _inventory = GetComponent<InventoryPlayer>();
        }

        private void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
            
            _playerInput.actions["PutTurel"].performed += OnSetDownTurel;

            _playerInput.actions["RotateTurel"].performed += OnRotateTurel;
            _playerInput.actions["RotateTurel"].canceled += OnStopRotateTurel;
            
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
            
            _playerInput.actions["PutTurel"].performed -= OnSetDownTurel;
            
            _playerInput.actions["RotateTurel"].performed -= OnRotateTurel;
            _playerInput.actions["RotateTurel"].canceled -= OnStopRotateTurel;
            
            _playerInput.actions["Move"].performed -= OnMove;
            _playerInput.actions["Move"].canceled -= OnMove;
            
            _playerInput.actions["Aim"].performed -= OnAim;
            _playerInput.actions["Aim"].canceled -= OnAim;
        }

        private void Update()
        {
            if (_isRotating)
            {
                _player.RotateTurel(RotateValue);      
            }
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

        private void OnMove(InputAction.CallbackContext context)
        {
            _player.SetParameter(context.ReadValue<Vector2>());
        }

        private void OnAim(InputAction.CallbackContext context)
        {
            _player.Aim(context.ReadValue<Vector2>());
        }

        private void OnRotateTurel(InputAction.CallbackContext context)
        {
            _isRotating = true;
            RotateValue = context.ReadValue<float>();
        }
        private void OnStopRotateTurel(InputAction.CallbackContext context)
        {
            _isRotating = false;
        }
        private void OnSetDownTurel(InputAction.CallbackContext context)
        {
            _inventory.SetDownTurel();
        }
    }
}
