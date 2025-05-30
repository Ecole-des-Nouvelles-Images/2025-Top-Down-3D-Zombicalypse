using Julien.Script.ZombieScript;
using Julien.Script.ZombieScript.States;

namespace Script.ZombieScript
{
    public class ZombieGeant : Zombie
    {
        private void Update()
        {
            switch (WantAttack)
            {
                case true when Dead:
                    CurrentState = new Dead();
                    break;
                case true when !CanAttack:
                    CurrentState = new GoAttackTarget();
                    break;
                case true when CanAttack:
                    CurrentState = new AttackTarget();
                    break;
            }
            
            CurrentState.Execute(this);
        }
    }
}
