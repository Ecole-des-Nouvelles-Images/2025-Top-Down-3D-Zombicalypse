namespace Julien.Script.ZombieScript.States
{
    public class Dead : ZombieState
    {
        public override void Execute(Zombie zombie)
        {
            zombie.NavMeshAgent.speed = 0;
        }
    }
}
