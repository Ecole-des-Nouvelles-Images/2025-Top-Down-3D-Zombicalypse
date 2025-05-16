using Script.Struc;
using TMPro;
using UnityEngine;

namespace Julien.Script.HUD
{
    public class HUDWeapon : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentAmmo; 
        [SerializeField] private TMP_Text _currentMagazin;
        
        public void SetHUD(WeaponWrap weapon)
        {
            _currentAmmo.text = weapon.CurrentAmmo.ToString();
            _currentMagazin.text = weapon.CurrentMagazin.ToString(); 
            
            // if (weapon.Weapon.InfiniteMagazine)
            // {
            //     _currentMagazin.text = "∞";
            //     Debug.Log("Balle pas infinie");
            // }
            // else
            // {
            //     _currentMagazin.text = weapon.CurrentMagazin.ToString(); 
            //     Debug.Log("Balle infinie");
            // }
        }
    }
}
