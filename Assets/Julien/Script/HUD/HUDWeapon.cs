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
            _currentMagazin.text = equipedWeapon.CurrentMagazin.ToString();
        }
    }
}
