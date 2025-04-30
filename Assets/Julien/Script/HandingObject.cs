using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class HandingObject : MonoBehaviour
    {
        [FormerlySerializedAs("_inventary")] [SerializeField] private InventoryPlayer inventory;

        [SerializeField] private SetSpawnBulletPosition _setSpawnBulletPosition;
        [SerializeField] private GameObject _weaponMesh;
        public GameObject TurelMesh;
        
        public void SwitchWeapon(Transform handingTransform)
        { 
            Destroy(_weaponMesh);
           _weaponMesh = Instantiate(inventory.equipedWeaponWrap.Weapon.WeaponMesh,  handingTransform.position + inventory.equipedWeaponWrap.Weapon.SpawnPositionOffset , handingTransform.transform.rotation, transform);
           _setSpawnBulletPosition.SetPosition(handingTransform.position + inventory.equipedWeaponWrap.Weapon.SpawnBulletOffset);
        }

        public void HandingTurel()
        {
            
        }
    }
}
