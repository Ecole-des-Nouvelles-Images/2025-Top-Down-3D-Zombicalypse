using System.Collections.Generic;
using Julien.Script.PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Julien.Script.Multiplayer
{
    public class MultiplayerInputHandler : MonoBehaviour
    {
       [SerializeField] private MultiplayerHandler _multiplayerHandler;
       [SerializeField] private Player _player;
       public int PlayerNumber;
       
        public static List<int> DiviceID = new List<int>(); 
        [SerializeField] private PlayerInput _playerInput;
        private void Start()
        {
            _player = GetComponent<Player>();
            _multiplayerHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MultiplayerHandler>();
            _multiplayerHandler.NumberOfPlayer++;
            _player.PlayerIndex = _multiplayerHandler.NumberOfPlayer;
            PlayerNumber = _multiplayerHandler.NumberOfPlayer;

            
            _playerInput = gameObject.GetComponent<PlayerInput>();
           DiviceID.Add(_playerInput.devices[0].deviceId);
           Debug.Log("Device Id = " + _playerInput.devices[0].deviceId);
        }
    }
}
