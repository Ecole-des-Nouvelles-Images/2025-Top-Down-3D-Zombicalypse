using Julien.Script.PlayerScripts;
using UnityEngine;

namespace Julien.Script.Data.Weapons
{
    [CreateAssetMenu(fileName = "Pistol", menuName = "Scriptable Objects/Weapon/ShootGun")]
    public class ShootGun : Weapon
    {
        public int BulletParShoot;
        public override void Fire(Transform spawnBulletPosition, PlayerScripts.Player player, Vector3 dir)
        {
            SoundManager.Instance.PlaySound( player.gameObject, SoundManager.Instance.Spas);
            
            for (int i = 0; i < BulletParShoot; i++)
            { 
                float randomFloat = Random.Range(1, 5);
                
                GameObject bullet = Instantiate(BulletPrefab, spawnBulletPosition.transform.position, spawnBulletPosition.transform.rotation);
                bullet.transform.forward = dir;
                bullet.gameObject.GetComponent<Bullet>().SetBulletParameter(BulletSpeed - randomFloat, Damage, Precision, LethalRangeLetal, player);
            }
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
