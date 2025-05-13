using Julien.Script.Interface;
using Script.ZombieScript.States;
using UnityEngine;

namespace Julien.Script.ZombieScript.States
{
    public class AttackTarget : ZombieState
    {
        public override void Execute(Zombie zombie)
        {
            zombie.AttackSpeed -= Time.deltaTime;
            zombie.NavMeshAgent.speed = 0;
            
            if (zombie.AttackSpeed <= 0)
            {
                zombie.Target.GetComponent<ITakeDamage>().takeDamage(zombie.Damage);
                zombie.AttackSpeed = zombie.TypeZombie.AttackSpeed;
            }
        }
    }
}
