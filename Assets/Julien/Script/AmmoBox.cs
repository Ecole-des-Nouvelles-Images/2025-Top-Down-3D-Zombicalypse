using Julien.Script.Interface;
using Julien.Script.PlayerScripts;
using UnityEngine;

namespace Julien.Script
{
    public class AmmoBox : MonoBehaviour, IInteractable
    {
        public void Activate(Player player)
        {
            int maxMagazin = player.Inventory.equipedWeaponWrap.CurrentMagazin + 1;
            if (maxMagazin <= player.Inventory.equipedWeaponWrap.Weapon.MaxMagazine)
            {
                player.Inventory.equipedWeaponWrap.CurrentMagazin++;
                player.hudPlayer.SetHudInfo();
                Destroy(gameObject); 
            }
          
        }
    }
}
