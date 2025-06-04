using Script.Turel.State;
using UnityEngine;

namespace Julien.Script.TurelScripts.State
{
    public class TurelShoot : TurelState
    {
        public override void Execute(Turel turel)
        {
            Debug.Log("Aim");
            if (turel.Target != null)
            {
                turel.AimTarget.transform.position = turel.Target.transform.position;
            }
            
            turel.TurelWrap.FireRate -= Time.deltaTime;
            if (turel.TurelWrap.FireRate <= 0)
            {
                Shoot(turel);
            }
        }

        public void Shoot(Turel turel)
        {
            SoundManager.Instance.PlaySound(turel.gameObject, SoundManager.Instance.TurelShoot, 0.3f);
            Debug.Log("Shoot");
            GameObject bullet = Instantiate(turel.TurelType.AmmoType, turel.SpawnBullet.transform.position, turel.TurelRenderer.transform.rotation);
            bullet.GetComponent<Bullet>().SetBulletParameter(turel.TurelWrap.BulletSpeed, turel.TurelWrap.Damage, turel.TurelWrap.Precision, turel.TurelWrap.BulletRange, null);
            bullet.transform.forward = turel.SpawnBullet.transform.forward;
            bullet.GetComponent<Bullet>().Impulse();
            turel.VisualEffect();
            turel.TurelWrap.FireRate = turel.TurelWrap.MaxFireRate;
        }
    }
}
