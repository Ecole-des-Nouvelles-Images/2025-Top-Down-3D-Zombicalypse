using System.Collections;
using Julien.Script.Data.Upgrader;
using Julien.Script.Struc;
using Julien.Script.TurelScripts;
using Script;
using Script.Struc;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Julien.Script
{
    
    public class InventoryPlayer : MonoBehaviour
    {
         public WeaponWrap equipedWeaponWrap;
         
         public WeaponWrap[] StrucWeapons;
         public int indexWeapon;

         public TurelWrap TurelWrap;
         public UpgraderWrap UpgraderWrap;
         
         [Header("Ref script")]
         
         [SerializeField] private Player _player;
         [SerializeField] private HandingObject handingObject;
        
         //[Header("Prefab")]
         //[SerializeField] private GameObject _weaponPrefab;
         
        private void Start()
        {
            _player = GetComponent<Player>();
            for (int i = 0; i < StrucWeapons.Length; i++)
            {
                if (StrucWeapons[i].Weapon != null)
                {
                    StrucWeapons[i].SetFirstData(); 
                }
            }
            handingObject.SwitchWeapon();
            equipedWeaponWrap = StrucWeapons[0];
        }
        
        public void Reload()
        {
            if (equipedWeaponWrap.CurrentMagazin - 1 !>= 0 && equipedWeaponWrap.CurrentAmmo != equipedWeaponWrap.Weapon.MaxAmmo && ! _player.isReloading)
            {
                StartCoroutine("ReloadDelay", equipedWeaponWrap.Weapon.ReloadTime);
            }
        }
        
        private IEnumerator ReloadDelay(float timer)
        {
            _player.isReloading = true;
            yield return new WaitForSeconds(timer);
            _player.isReloading = false;
            equipedWeaponWrap.CurrentAmmo = equipedWeaponWrap.Weapon.MaxAmmo;
            equipedWeaponWrap.CurrentMagazin--;
        }

        public void Switch()
        {
            int nextIndexWeapon = indexWeapon + 1 >= StrucWeapons.Length ? 0 : indexWeapon + 1;
            
            if (StrucWeapons[indexWeapon].Weapon != null && StrucWeapons[nextIndexWeapon].Weapon != null)
            {
                StrucWeapons[indexWeapon] = equipedWeaponWrap;
                equipedWeaponWrap = StrucWeapons[nextIndexWeapon];
                indexWeapon = nextIndexWeapon;
            }
        }

        public void AutomaticSwitch()
        {
            for (int i = 0; i < StrucWeapons.Length; i++)
            {
                if (StrucWeapons[i].Weapon != null)
                {
                    equipedWeaponWrap = StrucWeapons[i];
                    handingObject.SwitchWeapon();
                    indexWeapon = i;
                }
            }
        }
        
        public void SwitchWeapon()
        {
            Switch();
            StopCoroutine("ReloadDelay");
            handingObject.SwitchWeapon();
            _player.isReloading = false;
        }
 
        public void TookWeapon(WeaponWrap weaponWrap, GameObject weaponVisual)
        {
            for (int i = 0; i < StrucWeapons.Length; i++)
            {
                if (StrucWeapons[i].Weapon == null)
                {
                    StrucWeapons[i] = weaponWrap;
                    Destroy(weaponVisual);
                }
            }
        }

        public void DropWeapon()
        {
            int index = StrucWeapons.Length;
            
            for (int i = 0; i < StrucWeapons.Length; i++)
            {
                if (StrucWeapons[i].Weapon == null)
                {
                    index--;
                }
            }

            if (index > 1)
            {
                GameObject weapon = Instantiate(equipedWeaponWrap.WeaponPrefab, gameObject.transform.position, quaternion.identity);
                weapon.GetComponent<WeaponOnGround>().Drop(equipedWeaponWrap);
                weapon.GetComponent<WeaponOnGround>().DropedWeapon = true;
                StrucWeapons[indexWeapon].ClearData();
                equipedWeaponWrap.ClearData();
                AutomaticSwitch();
                Debug.Log("DropWeapon"); 
            }
        }
        
        public void SetDownTurel()
        {
            if (handingObject.HandingTurel.transform.GetChild(0).GetComponent<HologramTurel>().CanBeSetDoawn)
            {
                GameObject turel = Instantiate(TurelWrap.TurelType.Prefab, handingObject.HandingTurel.transform.GetChild(0).position, handingObject.HandingTurel.transform.GetChild(0).rotation);
                turel.GetComponent<Turel>().SetParameters(TurelWrap);
                Destroy(handingObject.HandingTurel.transform.GetChild(0).gameObject);
            
                handingObject.HandingWeapon.SetActive(true);
                _player.SwitchInputHandler(0);
                TurelWrap = new TurelWrap();
            }
        }
        
        public void PutBonus()
        {
            if (_player.currentAimTurel)
            {
                //_player.currentAimTurel.GetComponent<Turel>().TurelWrap.AddBonus(UpgraderWrap);
                _player.currentAimTurel.GetComponent<Turel>().UpdateTurel(UpgraderWrap);
                UpgraderWrap = new UpgraderWrap();
                Destroy(handingObject.HandingBonus.transform.GetChild(0).gameObject);
                handingObject.HandingWeapon.SetActive(true);
                _player.SwitchInputHandler(0);
            }
        }

        public void DropBonus()
        {
            GameObject bonus = Instantiate(UpgraderWrap.UpgraderPrefab, transform.position, quaternion.identity);
            bonus.GetComponent<BonusTurel>().UpgraderWrap = UpgraderWrap;
            Destroy(handingObject.HandingBonus.transform.GetChild(0).gameObject);
            handingObject.HandingWeapon.SetActive(true);
            _player.SwitchInputHandler(0);
        }
    }
}
