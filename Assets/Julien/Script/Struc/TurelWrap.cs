using System;
using Julien.Script.Data.Turel;
using UnityEngine;
using UnityEngine.Serialization;

namespace Julien.Script.Struc
{
    [Serializable]
    public struct TurelWrap
    {
        public TurelData TurelType;

        public int MaxAmmo;
        public float BulletSpeed;
        public float BulletRange;
        public float Damage;
        public float MaxFireRate;
        public float FireRate;
        public float Precision;
        public float Range;
        public float Distance;
        public float MaxHEalth;
        public float Health;
        public GameObject AmmoType;
        
        public void SetFirstData()
        {
            MaxAmmo = TurelType.MaxAmmo;
            BulletSpeed = TurelType.BulletSpeed;
            BulletRange = TurelType.BulletRange;
            Damage = TurelType.Damage;
            MaxFireRate = TurelType.MaxFireRate;
            FireRate = TurelType.FireRate;
            Precision = TurelType.Precision; 
            Range = TurelType.Range;
            Distance = TurelType.Distance;
            MaxHEalth = TurelType.MaxHealth;
            Health = TurelType.MaxHealth;
            AmmoType = TurelType.AmmoType;
        }

        public void AddBonus(UpgraderWrap BonusTurel)
        {
            Damage += BonusTurel.AddDamage;
            FireRate -= BonusTurel.AddFireRate;
            MaxHEalth += BonusTurel.AddMaxHealth;
        }
    }
}
