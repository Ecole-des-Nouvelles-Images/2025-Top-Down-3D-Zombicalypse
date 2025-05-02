using System;
using Julien.Script.Data.Upgrader;

namespace Julien.Script.Struc
{
    [Serializable]
    public struct UpgraderWrap
    {
        public Upgrader Upgrader;
        
        public float AddDamage;
        public float AddFireRate;
        public float AddMaxHealth;
        public float AddRange;
        public float AddDistance;

        public void SetData()
        {
            AddDamage = Upgrader.AddDamage;
            AddFireRate = Upgrader.AddFireRate;
            AddMaxHealth = Upgrader.AddMaxHealth;
            AddRange = Upgrader.AddRange;
            AddDistance = Upgrader.AddDistance;
        }
    }
}
