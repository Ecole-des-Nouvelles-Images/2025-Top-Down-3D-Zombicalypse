using Script.ZombieScript.States;
using Script.ZombieScript.States.Kamaikaz;

namespace Script.ZombieScript
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
            SetData();
            SetRoundBonusStat();
        }
    }
}
