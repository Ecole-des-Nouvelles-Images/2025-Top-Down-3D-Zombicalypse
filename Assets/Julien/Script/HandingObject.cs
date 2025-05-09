using Julien.Script.PlayerScripts;
using Script;
using UnityEngine;

namespace Julien.Script
{
    public class HandingObject : MonoBehaviour
    {
        [SerializeField] private InventoryPlayer inventory;

        public GameObject HandingWeapon;
        public GameObject HandingTurel;
        public GameObject HandingBonus;
        
        [SerializeField] private SetSpawnBulletPosition _setSpawnBulletPosition;
        [SerializeField] private GameObject _weaponMesh;
        public GameObject TurelPrefab;
        public GameObject BonusPrefab;
        
        public void SwitchWeapon()
        { 
            Destroy(_weaponMesh);
           _weaponMesh = Instantiate(inventory.equipedWeaponWrap.Weapon.WeaponMesh,  HandingWeapon.transform.position + inventory.equipedWeaponWrap.Weapon.SpawnPositionOffset , HandingWeapon.transform.rotation, HandingWeapon.transform);
           _setSpawnBulletPosition.SetPosition(HandingWeapon.transform.position + inventory.equipedWeaponWrap.Weapon.SpawnBulletOffset);
        }
    }
}
