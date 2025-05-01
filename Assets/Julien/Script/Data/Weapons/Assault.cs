using UnityEngine;

namespace Script.Data.WeaponsData
{
    [CreateAssetMenu(fileName = "Pistol", menuName = "Scriptable Objects/Weapon/Assault")]
    public class Assault : Weapon
    {
        public override void Fire(Transform spawnBulletPosition)
        {
            GameObject bullet = Instantiate(BulletPrefab, spawnBulletPosition.transform.position, spawnBulletPosition.transform.rotation);
            bullet.gameObject.GetComponent<Bullet>().SetBulletParameter(BulletSpeed, Damage, Precision, LethalRangeLetal);
        }

        public override void Reload()
        {
            throw new System.NotImplementedException();
        }
    }
}
