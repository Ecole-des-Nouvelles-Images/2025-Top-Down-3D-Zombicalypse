using Script.ZombieScript.States;
using UnityEngine;

namespace Julien.Script.ZombieScript.States
{
    public class GoAttackTarget : ZombieState
    {
        public override void Execute(Zombie zombie)
        {
            zombie.NavMeshAgent.SetDestination(zombie.Target.transform.position);
        }
    }
}
