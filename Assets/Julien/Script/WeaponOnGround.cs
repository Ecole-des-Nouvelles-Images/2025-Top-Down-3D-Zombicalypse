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
        [SerializeField] private List<Weapon> _possibleWeapon;
        [SerializeField] private List<Weapon> _weapons;
        [SerializeField] private GameObject _colorGameObject;
        
        public WeaponWrap weaponWrap;
        public bool DropedWeapon;
        
        [Header("Visual")]
        [SerializeField] private GameObject _visual;

        [SerializeField] private bool _random;
        
        private void Start()
        {
            if (!DropedWeapon)
            {
                //RandomWeapon();
                if (_random)
                {
                    weaponWrap.Weapon = GetRandomWeapon();
                }
                Debug.Log(GetRandomWeapon().lvl);
                weaponWrap.SetFirstData();
                SetVisual();
            }
        }

        public Weapon GetRandomWeapon()
        {
            _possibleWeapon.Sort((a, b) => a.DropChance.CompareTo(b.DropChance));
            
            float total = 0f;

            foreach (var weapon in _possibleWeapon)
            {
                total += weapon.DropChance;
            }

            float randomValue = Random.Range(0f, total);
            float cumulative = 0f;

            foreach (var weapon in _possibleWeapon)
            {
                cumulative += weapon.DropChance;
                if (randomValue <= cumulative)
                    return weapon;
                
            }

            return null;
        }
        
        private void RandomWeapon()
        {
            int index = Random.Range(0, _weapons.Count);
            float rand = Random.Range(0f, 100f);
            Debug.Log(rand);
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

            _colorGameObject.GetComponent<MeshRenderer>().material.color = weaponWrap.Color;
            _colorGameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", weaponWrap.Color * 0.2f);
            GameObject weapon = Instantiate(weaponWrap.Weapon.WeaponMesh, transform.position + new Vector3(0,0,0), quaternion.identity, transform);
        }
    }
}
