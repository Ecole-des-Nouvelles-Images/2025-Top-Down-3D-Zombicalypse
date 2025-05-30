using Julien.Script.PlayerScripts;
using UnityEngine;

namespace Julien.Script.Data.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
    public abstract class Weapon : ScriptableObject
    {
        public int lvl;
        public Color Color;

        [Range(0,100f)] public float DropChance;
        
        public GameObject Prefab;
        public bool Handgun;
        // visual Of weapon
        // le visuel contient aussi, la ou les balles spawn et deux point ou le joueur placera ses mains
        public GameObject WeaponMesh;
        public Sprite Sprite;
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
        public bool InfiniteMagazine;
        public int MaxMagazine;
        public float ReloadTime;

        public int RemoveAmmoParFire;
        public abstract void Fire(Transform spawnBulletPosition, Player player, Vector3 dir);
        public abstract void Reload();

        public abstract void VisualEffect(HandingObject handingObject);
    }
}
