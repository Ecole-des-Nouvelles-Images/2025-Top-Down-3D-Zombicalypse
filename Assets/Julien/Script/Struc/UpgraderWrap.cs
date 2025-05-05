using System;
using Julien.Script.Data.Upgrader;
using UnityEngine;
using UnityEngine.Serialization;

namespace Julien.Script.Struc
{
    [Serializable]
    public struct UpgraderWrap
    {
        public GameObject UpgraderPrefab;
        
        public Upgrader UpgraderType;
        
        public float AddDamage;
        public float AddFireRate;
        public float AddMaxHealth;
        public float AddRange;
        public float AddDistance;

        public void SetData()
        {
            UpgraderPrefab = UpgraderType.Prefab;
            AddDamage = UpgraderType.AddDamage;
            AddFireRate = UpgraderType.AddFireRate;
            AddMaxHealth = UpgraderType.AddMaxHealth;
            AddRange = UpgraderType.AddRange;
            AddDistance = UpgraderType.AddDistance;
        }
    }
}
