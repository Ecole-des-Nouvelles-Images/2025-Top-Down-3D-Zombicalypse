using UnityEngine;

namespace Julien.Script.Data.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
    public abstract class Weapon : ScriptableObject
    {
        public GameObject Prefab;
        // visual Of weapon
        // le visuel contient aussi, la ou les balles spawn et deux point ou le joueur placera ses mains
        public GameObject WeaponMesh;
        public Vector3 SpawnPositionOffset;
        public Vector3 SpawnBulletOffset;
        
        public GameObject BulletPrefab;

        [Header("Stat")]
        public float BulletSpeed;
        public float Damage;
        public float FireRate;
        
        public float Precision;
        public float LethalRangeLetal;
        
        public int MaxAmmo;
        public int MaxMagazine;
        public float ReloadTime;

        public int RemoveAmmoParFire;
        public abstract void Fire(Transform spawnBulletPosition);
        public abstract void Reload();
    }
}
