using System.Collections.Generic;
using Julien.Script.Data.Weapons;
using Julien.Script.Interface;
using Julien.Script.PlayerScripts;
using Script.Struc;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Julien.Script
{
    public class WeaponOnGround : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<Weapon> _weapons;
        
        public WeaponWrap weaponWrap;
        public bool DropedWeapon;
        
        [Header("Visual")]
        [SerializeField] private GameObject _visual;

        
        private void Start()
        {
            if (!DropedWeapon)
            {
                RandomWeapon();
                weaponWrap.SetFirstData();
                SetVisual();
            }
        }

        private void RandomWeapon()
        {
            int index = Random.Range(0, _weapons.Count);
            weaponWrap.Weapon = _weapons[index];
        }

        public void Drop(WeaponWrap weaponWrap)
        {
            this.weaponWrap = weaponWrap;
            SetVisual();
        }
        
        public void Activate(Player player)
        {
            Debug.Log("Take Weapon");
            player.GetComponent<InventoryPlayer>().TookWeapon(weaponWrap, gameObject);
        }

        private void SetVisual()
        {
            Destroy(transform.GetChild(0).gameObject);
            
            float randRotation = Random.Range(0f, 360f);
            Debug.Log(randRotation); 
            GameObject weapon = Instantiate(weaponWrap.Weapon.WeaponMesh, transform.position + new Vector3(0,1,0), quaternion.identity, transform);
        }
    }
}
