using Script.Struc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Julien.Script.HUD
{
    public class HUDWeapon : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        [SerializeField] private TMP_Text _currentAmmo; 
        [SerializeField] private TMP_Text _currentMagazin;
        
        public void SetHUD(WeaponWrap weapon)
        {
            _image.sprite = weapon.Sprite;
            _currentAmmo.text = weapon.CurrentAmmo.ToString();
            _currentMagazin.text = weapon.CurrentMagazin.ToString();
            //_image.color = weapon.Weapon.Color;

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
