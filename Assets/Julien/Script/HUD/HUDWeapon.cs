using Script.Struc;
using TMPro;
using UnityEngine;

namespace Julien.Script.HUD
{
    public class HUDWeapon : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentAmmo; 
        [SerializeField] private TMP_Text _currentMagazin;
        
        public void SetHUD(WeaponWrap equipedWeapon)
        {
            _currentAmmo.text = equipedWeapon.CurrentAmmo.ToString();
            if (!equipedWeapon.Weapon.InfiniteMagazine)
            {
                _currentMagazin.text = equipedWeapon.CurrentMagazin.ToString(); 
                Debug.Log("Balle pas infinie");
            }
            else
            {
                _currentMagazin.text = "∞";
                Debug.Log("Balle infinie");
            }
        }
    }
}
