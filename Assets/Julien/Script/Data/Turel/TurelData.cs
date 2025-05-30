using UnityEngine;

namespace Julien.Script.Data.Turel
{
    [CreateAssetMenu(fileName = "TurelData", menuName = "Scriptable Objects/TurelData")]
    public class TurelData : ScriptableObject
    {
        public int MaxAmmo;
        public float Damage;
        public float MaxFireRate;
        public float FireRate;
        public float Precision;
        public float Range;
        public float Distance;
        public float BulletSpeed;
        public float MaxHealth;
        public float BulletRange;
        public GameObject AmmoType;
        public GameObject Prefab;
        public GameObject OnGroundPrefab;
        public GameObject VisualHologram;

    }
}
