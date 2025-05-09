using Julien.Script.PlayerScripts;
using UnityEngine;

namespace Julien.Script.Data.Weapons
{
    [CreateAssetMenu(fileName = "Pistol", menuName = "Scriptable Objects/Weapon/ShootGun")]
    public class ShootGun : Weapon
    {
        public int BulletParShoot;
        public override void Fire(Transform spawnBulletPosition, Player player)
        {
            for (int i = 0; i < BulletParShoot; i++)
            { 
                float randomFloat = Random.Range(1, 5);
                
                GameObject bullet = Instantiate(BulletPrefab, spawnBulletPosition.transform.position, spawnBulletPosition.transform.rotation);
                bullet.gameObject.GetComponent<Bullet>().SetBulletParameter(BulletSpeed - randomFloat, Damage, Precision, LethalRangeLetal, player);
            }
        }

        public override void Reload()
        {
            throw new System.NotImplementedException();
        }
    }
}
