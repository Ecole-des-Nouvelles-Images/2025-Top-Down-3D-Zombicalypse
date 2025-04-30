using UnityEngine;

namespace Script.Data.TurellData
{
    [CreateAssetMenu(fileName = "TurelData", menuName = "Scriptable Objects/TurelData")]
    public class TurelData : ScriptableObject
    {
        public int MaxAmmo;
        public float Damage;
        public float FireRate;
        public float Precision;
        public float Range;
        public float BulletSpeed;
        public float MaxHealth;
        public float BulletRange;
        public GameObject AmmoType;
        public GameObject Visual;
    }
}
