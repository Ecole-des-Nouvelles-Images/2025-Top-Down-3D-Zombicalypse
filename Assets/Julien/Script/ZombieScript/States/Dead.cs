using UnityEngine;

namespace Julien.Script.ZombieScript.States
{
    public class Dead : ZombieState
    {
        public override void Execute(Zombie zombie)
        {
            zombie.NavMeshAgent.speed = 0;
            zombie.WantAttack = false;
            zombie.CanAttack = false;
            zombie.NavMeshAgent.enabled = false;
            zombie.GetComponent<CapsuleCollider>().enabled = false;
            zombie.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
