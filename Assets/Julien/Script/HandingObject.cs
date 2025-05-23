using System;
using Julien.Script.PlayerScripts;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

namespace Julien.Script
{
    public class HandingObject : MonoBehaviour
    {
        [SerializeField] private InventoryPlayer inventory;

        public GameObject HandingWeapon;
        public GameObject HandingTurel;
        public GameObject HandingBonus;
        
        [SerializeField] private SetSpawnBulletPosition _setSpawnBulletPosition;
        public GameObject WeaponMesh;
        public GameObject TurelPrefab;
        public GameObject BonusPrefab;

        [SerializeField] private GameObject _bonesPlayer;
        
        [SerializeField] private float rotation;
        public void SwitchWeapon()
        { 
            Destroy(WeaponMesh);
           WeaponMesh = Instantiate(inventory.equipedWeaponWrap.Weapon.WeaponMesh,  HandingWeapon.transform.position + inventory.equipedWeaponWrap.Weapon.SpawnPositionOffset , HandingWeapon.transform.rotation, HandingWeapon.transform);
           _setSpawnBulletPosition.SetPosition(HandingWeapon.transform.position + inventory.equipedWeaponWrap.Weapon.SpawnBulletOffset);
        }

        private void Update()
        {
            RotateWeapon();
        }

        public void RotateWeapon()
        {
            WeaponMesh.transform.LookAt(_bonesPlayer.transform, Vector3.forward);
        }
    }
}
