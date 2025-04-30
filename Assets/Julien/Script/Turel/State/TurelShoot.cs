using UnityEngine;

namespace Script.Turel.State
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
            
            turel.FireRate -= Time.deltaTime;
            if (turel.FireRate <= 0)
            {
                Shoot(turel);
            }
        }

        public void Shoot(Turel turel)
        {
            Debug.Log("Shoot");
            GameObject bullet = Instantiate(turel.TurelType.AmmoType, turel.SpawnBullet.transform.position, turel.TurelRenderer.transform.rotation);
            bullet.GetComponent<Bullet>().SetBulletParameter(turel.BulletSpeed, turel.Damage, turel.Precision, turel.BulletRange);
            bullet.GetComponent<Bullet>().Impulse();
            turel.FireRate = turel.MaxFireRate;
        }
    }
}
