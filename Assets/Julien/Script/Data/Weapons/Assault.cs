using Julien.Script.PlayerScripts;
using UnityEngine;

namespace Julien.Script.Data.Weapons
{
    [CreateAssetMenu(fileName = "Pistol", menuName = "Scriptable Objects/Weapon/Assault")]
    public class Assault : Weapon
    {
        public override void Fire(Transform spawnBulletPosition, Player player)
        {
            GameObject bullet = Instantiate(BulletPrefab, spawnBulletPosition.transform.position, spawnBulletPosition.transform.rotation);
            bullet.gameObject.GetComponent<Bullet>().SetBulletParameter(BulletSpeed, Damage, Precision, LethalRangeLetal, player);
        }

        public override void Reload()
        {
            throw new System.NotImplementedException();
        }
    }
}
