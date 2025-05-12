using Julien.Script.ZombieScript.States;
using Julien.Script.ZombieScript.States.Kamaikaz;
using Script.ZombieScript.States;

namespace Julien.Script.ZombieScript
{
    public class ZombieKamikaz : Zombie
    {
        private void Update()
        {
            switch (WantAttack)
            {
                case true when !CanAttack:
                    CurrentState = new GoAttackTarget();
                    break;
                case true when CanAttack:
                    CurrentState = new GoExplose();
                    break;
            }
            
            
            CurrentState.Execute(this);
        }
        private void Start()
        {
            if (Targets[0] == null)
            {
                TargetTag = "Generator";
            }
            else
            {
                TargetTag = "Player";
            }
            
            SetData();
            SetRoundBonusStat();
        }
    }
}
