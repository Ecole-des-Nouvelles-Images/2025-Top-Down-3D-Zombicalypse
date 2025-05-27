using Julien.Script.Static;
using Julien.Script.ZombieScript.States;
using Julien.Script.ZombieScript.States.Kamaikaz;
using UnityEngine;

namespace Julien.Script.ZombieScript
{
    public class ZombieKamikaz : Zombie
    {
        private void Update()
        {
            if (GameManagerStatic.Players.Count > 1)
            {
                Debug.Log("Focus les joueur");
            }
            else if (GameManagerStatic.Players.Count == 0)
            {
                TargetTag = "Generator";
                Target = GameObject.FindWithTag("Generator");
                Debug.Log("Focus le générateur");
            }
            
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
