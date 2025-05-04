using Julien.Script.ZombieScript;
using UnityEngine;

namespace Script.ZombieScript.States
{
    public class GoAttackTarget : ZombieState
    {
        public override void Execute(Zombie zombie)
        {
            zombie.NavMeshAgent.SetDestination(zombie.Target.transform.position);

            if (Vector3.Distance(zombie.gameObject.transform.position, zombie.Target.transform.position) <= zombie.AttackRange)
            {
                zombie.CanAttack = true;
            }
        }
    }
}
