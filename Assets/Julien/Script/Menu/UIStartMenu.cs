using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Julien.Script.Menu
{
    public class UIStartMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _panelPause;
        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
            List<GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();
            foreach (GameObject player in players)
            {
                Destroy(player);
            }
        }

        public void Return()
        {
            _panelPause.SetActive(false);
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}
