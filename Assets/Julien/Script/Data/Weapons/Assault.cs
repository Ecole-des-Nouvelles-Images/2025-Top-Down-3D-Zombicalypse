using Julien.Script.PlayerScripts;
using UnityEngine;

namespace Julien.Script.Data.Weapons
{
    [CreateAssetMenu(fileName = "Pistol", menuName = "Scriptable Objects/Weapon/Assault")]
    public class Assault : Weapon
    {
        public override void Fire(Transform spawnBulletPosition, Player player, Vector3 dir)
        {
            GameObject bullet = Instantiate(BulletPrefab, spawnBulletPosition.transform.position, spawnBulletPosition.transform.rotation);
            bullet.transform.LookAt(dir);
            bullet.gameObject.GetComponent<Bullet>().SetBulletParameter(BulletSpeed, Damage, Precision, LethalRangeLetal, player);
            
            SoundManager.Instance.PlaySound( player.gameObject, SoundManager.Instance.Ak47);
        }

        public override void Reload()
        {
            throw new System.NotImplementedException();
        }

        public override void VisualEffect(HandingObject handingObject)
        {
            Transform bulletParticle = handingObject.WeaponMesh.transform.Find("BulletFallVfx");
            bulletParticle.gameObject.GetComponent<ParticleSystem>().Play();
                
            Transform flashParticle = handingObject.WeaponMesh.transform.Find("FlashTurretVfx");
            flashParticle.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
}
