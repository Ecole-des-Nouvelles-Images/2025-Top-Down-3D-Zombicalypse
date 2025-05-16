using System.Collections;
using Julien.Script.Struc;
using Julien.Script.TurelScripts;
using Script.Struc;
using Unity.Mathematics;
using UnityEngine;

namespace Julien.Script.PlayerScripts
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
            if (equipedWeaponWrap.CurrentMagazin - 1 !>= 0 && equipedWeaponWrap.CurrentAmmo != equipedWeaponWrap.Weapon.MaxAmmo && ! _player.isReloading || equipedWeaponWrap.Weapon.InfiniteMagazine)
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
            _player.hudPlayer.SetHudInfo();
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
            StrucWeapons[indexWeapon] = equipedWeaponWrap;
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
                    _player.hudPlayer.SetActiveWeapon(i, true);
                    Destroy(weaponVisual);
                }
            }
            AutomaticSwitch();
            _player.hudPlayer.SetAllInfoHud();
        }

        public void DropWeapon()
        {
            int indexCount = StrucWeapons.Length;
            
            
            for (int i = 0; i < StrucWeapons.Length; i++)
            {
                if (StrucWeapons[i].Weapon == null)
                {
                    indexCount--;
                }
            }

            if (indexCount > 1)
            {
                Debug.Log("Va drop l'arme");
                GameObject weapon = Instantiate(equipedWeaponWrap.WeaponPrefab, gameObject.transform.position, quaternion.identity);
                weapon.GetComponent<WeaponOnGround>().Drop(equipedWeaponWrap);
                weapon.GetComponent<WeaponOnGround>().DropedWeapon = true;
                _player.hudPlayer.SetActiveWeapon(indexWeapon, false);
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
                _player.currentAimTurel.GetComponent<Turel>().UpdateTurel(UpgraderWrap);
                UpgraderWrap = new UpgraderWrap();
                Destroy(handingObject.HandingBonus.transform.GetChild(0).gameObject);
                handingObject.HandingWeapon.SetActive(true);
                _player.currentAimTurel = null;
                _player.SwitchInputHandler(0);
            }
        }

        public void DropBonus()
        {
            GameObject bonus = Instantiate(UpgraderWrap.UpgraderPrefab, transform.position, quaternion.identity);
            bonus.GetComponent<BonusTurel>().UpgraderWrap = UpgraderWrap;
            Destroy(handingObject.HandingBonus.transform.GetChild(0).gameObject);
            handingObject.HandingWeapon.SetActive(true);
            UpgraderWrap = new UpgraderWrap();
            bonus.GetComponent<BonusTurel>().SetVisual();
            _player.SwitchInputHandler(0);
        }
    }
}
