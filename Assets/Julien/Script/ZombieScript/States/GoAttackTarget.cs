using Script.ZombieScript.States;

namespace Julien.Script.ZombieScript.States
{
    public class GoAttackTarget : ZombieState
    {
        public override void Execute(Zombie zombie)
        {
            zombie.NavMeshAgent.SetDestination(zombie.Target.transform.position);
            zombie.Animator.SetBool("Run", true);
        }
    }
}
