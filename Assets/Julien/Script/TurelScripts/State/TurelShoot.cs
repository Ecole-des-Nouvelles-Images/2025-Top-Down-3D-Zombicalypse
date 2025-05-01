using Script;
using Script.Turel.State;
using UnityEngine;

namespace Julien.Script.TurelScripts.State
{
    public class TurelShoot : TurelState
    {
        public override void Execute(Julien.Script.TurelScripts.Turel turel)
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

        public void Shoot(Julien.Script.TurelScripts.Turel turel)
        {
            Debug.Log("Shoot");
            GameObject bullet = Instantiate(turel.TurelType.AmmoType, turel.SpawnBullet.transform.position, turel.TurelRenderer.transform.rotation);
            bullet.GetComponent<Bullet>().SetBulletParameter(turel.TurelWrap.BulletSpeed, turel.TurelWrap.Damage, turel.TurelWrap.Precision, turel.TurelWrap.BulletRange);
            bullet.GetComponent<Bullet>().Impulse();
            turel.TurelWrap.FireRate = turel.TurelWrap.MaxFireRate;
        }
    }
}
