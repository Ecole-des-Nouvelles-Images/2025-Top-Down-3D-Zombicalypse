using System;
using System.Collections.Generic;
using Julien.Script.Static;
using Unity.VisualScripting;
using UnityEngine;

namespace Julien.Script.HUD
{
    public class HUDPlayer : MonoBehaviour
    {
        public Player PlayerTarget;
        public InventoryPlayer Inventory;
        
        [SerializeField] private List<HUDWeapon> _weaponsHUD = new List<HUDWeapon>();
        public HUDPlayerHealth HUDPlayerHealth;
        public void SetInfoOnStart(Player player)
        {
            PlayerTarget = player;
            SetPosition();
            Inventory = PlayerTarget.GetComponent<InventoryPlayer>();
            SetAllInfoHud();
        }

        public void SetAllInfoHud()
        {
            _weaponsHUD[0].SetHUD(Inventory.StrucWeapons[0]);
            _weaponsHUD[1].SetHUD(Inventory.StrucWeapons[1]);
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

        public void SetHudInfo()
        {
            _weaponsHUD[Inventory.indexWeapon].GetComponent<HUDWeapon>().SetHUD(Inventory.equipedWeaponWrap);
            Debug.Log("Change info HUD");
        }

        public void SetActiveWeapon(int index, bool condition)
        {
            _weaponsHUD[index].gameObject.SetActive(condition);
        }
    }
}
