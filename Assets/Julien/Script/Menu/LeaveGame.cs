using Julien.Script.Interface;
using UnityEngine;

namespace Julien.Script.Menu
{
    public class LeaveGame : MonoBehaviour, IInteractable
    {
        public void Activate(Player player)
        {
            Destroy(player.gameObject);
        }
        
    }
}
