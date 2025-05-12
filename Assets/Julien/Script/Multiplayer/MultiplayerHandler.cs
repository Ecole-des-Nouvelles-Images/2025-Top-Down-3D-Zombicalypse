using System.Collections.Generic;
using UnityEngine;

namespace Julien.Script.Multiplayer
{
    public class MultiplayerHandler : MonoBehaviour
    {
        public int NumberOfPlayer;
        
        private void Update()
        {
            Debug.Log("Nombre de joueur " + NumberOfPlayer);
        }
    }
}
