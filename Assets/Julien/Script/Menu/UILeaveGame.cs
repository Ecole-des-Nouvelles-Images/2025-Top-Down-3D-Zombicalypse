using Julien.Script.Interface;
using UnityEngine;

namespace Julien.Script.Menu
{
    public class UILeaveGame : MonoBehaviour, IInteractable
    {
        public void Activate(Player player)
        {
            Destroy(player.gameObject);
        }
        
    }
}
