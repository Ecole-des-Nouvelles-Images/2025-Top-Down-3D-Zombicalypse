using Julien.Script.Interface;
using UnityEngine;

namespace Julien.Script.Menu
{
    public class UI_LeaveGame : MonoBehaviour, IInteractable
    {
        public void Activate(Player player)
        {
            Destroy(player.gameObject);
        }
        
    }
}
