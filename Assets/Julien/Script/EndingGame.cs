using System.Collections.Generic;
using System.Linq;
using Julien.Script.HUD;
using Julien.Script.PlayerScripts;
using UnityEngine;

namespace Julien.Script
{
    public class EndingGame : MonoBehaviour
    {
        [SerializeField] private HUDMultiplayerManager _hudMultiplayerManager;

        public void EndGame()
        {
            _hudMultiplayerManager.SetHUD();

            List <GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();
            foreach (GameObject player in players)
            {
                player.GetComponent<Player>().SwitchInputHandler(4);
            }
        }
    }
}
