using System.Collections;
using Julien.Script.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Julien.Script.Lobby
{
    public class StartGame : MonoBehaviour
    {
        public int CurrentNumberPlayer;


        private MultiplayerHandler _multiplayerHandler;
        
        private void Awake()
        {
            _multiplayerHandler = GameObject.FindWithTag("GameManager").GetComponent<MultiplayerHandler>();
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
            }
        }

        public IEnumerator StartGameCoroutine()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("Julien/Scenes/GameScene");
        }
    }
}
