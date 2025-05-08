using System.Collections.Generic;
using Julien.Script.Static;
using Script.Struc;
using UnityEngine;

namespace Julien.Script.HUD
{
    public class HUDPlayerInventory : MonoBehaviour
    {
        public Player PlayerTarget;
        public InventoryPlayer Inventory;
        
        [SerializeField] private List<HUDWeapon> _weaponsHUD = new List<HUDWeapon>();
        
        public void SetInfo(Player player)
        {
            PlayerTarget = player;
            SetPosition();
            Inventory = PlayerTarget.GetComponent<InventoryPlayer>();
        }

        public void SetPosition()
        {
            RectTransform hudRectTransform = GetComponent<RectTransform>();

            int index = PlayerTarget.PlayerIndex - 1;
            
            hudRectTransform.anchorMin = new Vector2(GameManagerStatic.Anchor[index].x, GameManagerStatic.Anchor[index].y);
            hudRectTransform.anchorMax = new Vector2(GameManagerStatic.Anchor[index].z, GameManagerStatic.Anchor[index].w);
            hudRectTransform.offsetMin = new Vector2(0, 0);
            hudRectTransform.offsetMax = new Vector2(0, 0);
        }

        public void SetHudInfo(WeaponWrap weaponWrap)
        {
            _weaponsHUD[Inventory.indexWeapon]
        }
    }
}
