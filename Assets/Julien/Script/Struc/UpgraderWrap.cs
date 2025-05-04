using System;
using Julien.Script.Data.Upgrader;
using UnityEngine;

namespace Julien.Script.Struc
{
    [Serializable]
    public struct UpgraderWrap
    {
        public GameObject UpgraderPrefab;
        
        public Upgrader Upgrader;
        
        public float AddDamage;
        public float AddFireRate;
        public float AddMaxHealth;
        public float AddRange;
        public float AddDistance;

        public void SetData()
        {
            UpgraderPrefab = Upgrader.Prefab;
            AddDamage = Upgrader.AddDamage;
            AddFireRate = Upgrader.AddFireRate;
            AddMaxHealth = Upgrader.AddMaxHealth;
            AddRange = Upgrader.AddRange;
            AddDistance = Upgrader.AddDistance;
        }
    }
}
