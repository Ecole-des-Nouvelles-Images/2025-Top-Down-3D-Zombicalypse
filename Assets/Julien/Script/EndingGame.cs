using Julien.Script.HUD;
using UnityEngine;

namespace Julien.Script
{
    public class EndingGame : MonoBehaviour
    {
        [SerializeField] private HUDMultiplayerManager _hudMultiplayerManager;

        public void EndGame()
        {
            _hudMultiplayerManager.SetHUD();
        }
    }
}
