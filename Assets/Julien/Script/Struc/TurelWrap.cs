using System;
using Script.Data.TurellData;
using UnityEngine;

namespace Julien.Script.Struc
{
    [Serializable]
    public struct TurelWrap
    {
        public TurelData Turel;

        public int MaxAmmo;
        public float BulletSpeed;
        public float BulletRange;
        public float Damage;
        public float MaxFireRate;
        public float FireRate;
        public float Precision;
        public float Range;
        public float MaxHEalth;
        public float Health;
        public GameObject AmmoType;
        
        public void SetFirstData()
        {
            MaxAmmo = Turel.MaxAmmo;
            BulletSpeed = Turel.BulletSpeed;
            BulletRange = Turel.BulletRange;
            Damage = Turel.Damage;
            MaxFireRate = Turel.MaxFireRate;
            FireRate = Turel.FireRate;
            Precision = Turel.Precision; 
            Range = Turel.Range;
            MaxHEalth = Turel.MaxHealth;
            Health = Turel.MaxHealth;
            AmmoType = Turel.AmmoType;
        }

        public void AddBonus(UpgraderWrap BonusTurel)
        {
            Damage += BonusTurel.AddDamage;
            FireRate -= BonusTurel.AddFireRate;
            MaxHEalth += BonusTurel.AddMaxHealth;
        }
    }
}
