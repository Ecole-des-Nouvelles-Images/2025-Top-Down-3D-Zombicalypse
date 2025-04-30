using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Multiplayer
{
    public class MultiplayerInputHandler : MonoBehaviour
    {
       [SerializeField] private MultiplayerHandler _multiplayerHandler;
       public int PlayerNumber;
       
        public static List<int> DiviceID = new List<int>(); 
        [SerializeField] private PlayerInput _playerInput;
        private void Start()
        {
            _multiplayerHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MultiplayerHandler>();
            _multiplayerHandler.NumberOfPlayer++;
            PlayerNumber = _multiplayerHandler.NumberOfPlayer;

            
            _playerInput = gameObject.GetComponent<PlayerInput>();
           DiviceID.Add(_playerInput.devices[0].deviceId);
           Debug.Log("Device Id = " + _playerInput.devices[0].deviceId);
        }
    }
}
