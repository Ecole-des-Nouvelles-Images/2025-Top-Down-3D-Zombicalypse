using System;
using System.Collections;
using Julien.Script.Multiplayer;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Julien.Script.Lobby
{
    public class StartGame : MonoBehaviour
    {
        public int CurrentNumberPlayer;
        
        private MultiplayerHandler _multiplayerHandler;

        [SerializeField] PlayerInputManager _playerInputManager;

        [SerializeField] private TMP_Text _text;
        private void Awake()
        {
            _multiplayerHandler = GameObject.FindWithTag("GameManager").GetComponent<MultiplayerHandler>();
        }

        private void OnEnable()
        {
            _playerInputManager.onPlayerJoined += OnPlayerJoin;
        }

        private void OnDisable()
        {
            _playerInputManager.onPlayerJoined -= OnPlayerJoin;
        }

        private void OnPlayerJoin(PlayerInput playerInput)
        {
            Debug.Log("Player join");
            _multiplayerHandler.NumberOfPlayer++;
            _text.text = CurrentNumberPlayer + "/" + _multiplayerHandler.NumberOfPlayer;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                CurrentNumberPlayer++;
                if (CurrentNumberPlayer >= _multiplayerHandler.NumberOfPlayer)
                {
                    StopCoroutine("StartGameCoroutine");
                    StartCoroutine("StartGameCoroutine");
                }

                _text.text = CurrentNumberPlayer + "/" + _multiplayerHandler.NumberOfPlayer;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                CurrentNumberPlayer--;
                if (CurrentNumberPlayer <= _multiplayerHandler.NumberOfPlayer)
                {
                    StopCoroutine("StartGameCoroutine");
                }

                _text.text = CurrentNumberPlayer + "/" + _multiplayerHandler.NumberOfPlayer;
            }
        }

        public IEnumerator StartGameCoroutine()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("GameScene");
        }
    }
}
