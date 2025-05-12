using Julien.Script.ZombieScript.States;
using Script.ZombieScript.States;

namespace Julien.Script.ZombieScript
{
    public class ZombieNormal : Zombie
    {
        private void Update()
        {
            switch (WantAttack)
            {
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
