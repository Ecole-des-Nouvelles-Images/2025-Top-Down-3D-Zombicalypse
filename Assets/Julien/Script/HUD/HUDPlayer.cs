using System.Collections.Generic;
using Julien.Script.PlayerScripts;
using Julien.Script.Static;
using UnityEngine;
using UnityEngine.Serialization;

namespace Julien.Script.HUD
{
    public class HUDPlayer : MonoBehaviour
    {
        [SerializeField] private Player _playerTarget;
        [SerializeField] private InventoryPlayer _inventory;
        
        [SerializeField] private List<HUDWeapon> _weaponsHUD = new List<HUDWeapon>();
        public HUDPlayerHealth HUDPlayerHealth;
        public void SetInfoOnStart(Player player)
        {
            _playerTarget = player;
            SetPosition();
            _inventory = _playerTarget.GetComponent<InventoryPlayer>();
            SetAllInfoHud();
        }

        public void SetAllInfoHud()
        {
            _weaponsHUD[0].SetHUD(_inventory.StrucWeapons[0]);
            _weaponsHUD[1].SetHUD(_inventory.StrucWeapons[1]);
        }

        public void SetPosition()
        {
            RectTransform hudRectTransform = GetComponent<RectTransform>();

            int index = _playerTarget.PlayerIndex - 1;
            
            hudRectTransform.anchorMin = new Vector2(GameManagerStatic.Anchors[index].x, GameManagerStatic.Anchors[index].y);
            hudRectTransform.anchorMax = new Vector2(GameManagerStatic.Anchors[index].z, GameManagerStatic.Anchors[index].w);
            hudRectTransform.offsetMin = new Vector2(0, 0);
            hudRectTransform.offsetMax = new Vector2(0, 0);
        }

        public void SetHudInfo()
        {
            _weaponsHUD[_inventory.indexWeapon].GetComponent<HUDWeapon>().SetHUD(_inventory.equipedWeaponWrap);
            Debug.Log("Change info HUD");
        }

        public void SetActiveWeapon(int index, bool condition)
        {
            _weaponsHUD[index].gameObject.SetActive(condition);
        }
    }
}
