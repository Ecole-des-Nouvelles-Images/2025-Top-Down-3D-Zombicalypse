using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Julien.Script.HUD
{
    public class HUDMultiplayerManager : MonoBehaviour
    {
        private RoundHundler _roundHundler;
        [SerializeField] private List<GameObject> _endGamesHUD = new List<GameObject>();
        [SerializeField] private TMP_Text _surviveText;

        private void Start()
        {
            _endGamesHUD = GameObject.FindGameObjectsWithTag("EndHud").ToList();
            foreach (GameObject hud in _endGamesHUD)
            {
                hud.SetActive(false);
            }
        }

        private void Awake()
        {
            _roundHundler = GameObject.FindWithTag("GameManager").gameObject.GetComponent<RoundHundler>();
        }

        public void SetHUD()
        {
            foreach (GameObject hud in _endGamesHUD)
            {
                hud.SetActive(true);
            }
            
            List<GameObject> CurrentHUD = GameObject.FindGameObjectsWithTag("Hud").ToList();
            foreach (GameObject hud in CurrentHUD)
            {
                hud.SetActive(false);
            }
            
            //_surviveText.text = "Fin de partie Vous avez survécu " + _roundHundler.Round.CurrentRound + " vagues";
        }

        public void MainMenu()
        {
            List<GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();
            foreach (GameObject player in players)
            {
                Destroy(player);
            }
            SceneManager.LoadScene("MainMenu");
        }
    }
}
