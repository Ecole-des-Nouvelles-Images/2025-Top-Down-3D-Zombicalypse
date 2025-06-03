using Julien.Script.Interface;
using UnityEngine;

namespace Julien.Script.ZombieScript.States
{
    public class AttackTarget : ZombieState
    {
        public override void Execute(Zombie zombie)
        {
            zombie.AttackSpeed -= Time.deltaTime;
            zombie.NavMeshAgent.speed = 0;
            zombie.Animator.SetBool("Run", false);
            
            if (zombie.AttackSpeed <= 0)
            {
                zombie.Target.GetComponent<ITakeDamage>().takeDamage(zombie.Damage);
                zombie.AttackSpeed = zombie.TypeZombie.AttackSpeed;
                zombie.Animator.SetTrigger("Attack");
                
                SoundManager.Instance.PlaySound(zombie.gameObject, SoundManager.Instance.AttackZombie);
            }
        }
    }
}
