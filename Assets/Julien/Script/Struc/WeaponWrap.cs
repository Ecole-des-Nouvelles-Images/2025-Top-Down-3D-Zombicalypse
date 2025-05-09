using System;
using Julien.Script.Data.Weapons;
using UnityEngine;

namespace Script.Struc
{
    [Serializable]
    public struct WeaponWrap
    {
        public GameObject WeaponPrefab;
        
        public Weapon Weapon;
        public int CurrentAmmo;
        public int CurrentMagazin;
        public void SetFirstData()
        {
            CurrentAmmo = Weapon.MaxAmmo;
            CurrentMagazin = Weapon.MaxMagazine;
            WeaponPrefab = Weapon.Prefab;
        }

        public void ClearData()
        {
            WeaponPrefab = null;
            Weapon = null;
            CurrentAmmo = 0;
            CurrentMagazin = 0;
        }
    }
}
