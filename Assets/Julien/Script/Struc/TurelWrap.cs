using System;
using Script.Data.TurellData;
using UnityEngine;

namespace Script.Struc
{
    [Serializable]
    public struct TurelWrap
    {
        public TurelData Turel;

        public int MaxAmmo;
        public float Damage;
        public float FireRate;
        public float Precision;
        public float Range;
        public float MaxHEalth;
        public GameObject AmmoType;
        
        public void SetFirstData()
        {
            MaxAmmo = Turel.MaxAmmo;
            Damage = Turel.Damage;
            FireRate = Turel.FireRate;
            Precision = Turel.Precision;
            Range = Turel.Range;
            MaxHEalth = Turel.MaxHealth;
            AmmoType = Turel.AmmoType;
        }
    }
}
