using System;
using Script.Struc;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    
    public class InventoryPlayer : MonoBehaviour
    {
         [FormerlySerializedAs("equipedStructWeapon")] public WeaponWrap equipedWeaponWrap;
         
         public WeaponWrap[] StrucWeapons;
         public int indexWeapon;

         public TurelWrap _TurelWrap;
         
         [FormerlySerializedAs("_weaponHanding")]
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
            handingObject.SwitchWeapon(handingObject.transform);
            equipedWeaponWrap = StrucWeapons[0];
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
                    handingObject.SwitchWeapon(handingObject.transform);
                    indexWeapon = i;
                }
            }
        }
 
        public void TookWeapon(WeaponWrap weaponWrap, GameObject weaponVisual)
        {
            for (int i = 0; i < StrucWeapons.Length; i++)
            {
                if (StrucWeapons[i].Weapon == null)
                {
                    StrucWeapons[i] = weaponWrap;
                    Destroy(weaponVisual);
                    Debug.Log(" met l'arme dans sont inventaire");
                }
            }
        }

        public void TookTurel(TurelWrap turelWrap, GameObject turelVisual)
        {
            _TurelWrap = turelWrap;
            _player.SwitchInputHandler(1);
            GameObject turel = Instantiate(turelVisual, handingObject.transform.position, Quaternion.identity, handingObject.transform);
            turel.GetComponent<BoxCollider>().enabled = false;
        }

        public void DropWeapon(GameObject dropPrefab)
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
                GameObject weapon = Instantiate(dropPrefab, gameObject.transform.position, quaternion.identity);
                weapon.GetComponent<WeaponOnFloor>().Drop(equipedWeaponWrap);
                weapon.GetComponent<WeaponOnFloor>().DropedWeapon = true;
                StrucWeapons[indexWeapon].ClearData();
                equipedWeaponWrap.ClearData();
                AutomaticSwitch();
                Debug.Log("DropWeapon"); 
            }
            
        }
    }
}
