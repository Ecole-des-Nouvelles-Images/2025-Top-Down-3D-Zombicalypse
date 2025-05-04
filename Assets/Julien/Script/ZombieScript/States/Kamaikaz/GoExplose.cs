using Script.ZombieScript;
using Script.ZombieScript.States;
using UnityEngine;

namespace Julien.Script.ZombieScript.States.Kamaikaz
{
    public class GoExplose : ZombieState
    {
        public override void Execute(Zombie zombie)
        {
            zombie.NavMeshAgent.speed = 0f;
        
            zombie.AttackSpeed -= Time.deltaTime;
        
            if (zombie.AttackSpeed <= 0)
            {
                if (zombie.Object != null)
                {
                    foreach (GameObject obj in zombie.Object)
                    {
                        if (obj.CompareTag("Player"))
                        {
                            obj.GetComponent<Player>().Health -= zombie.Damage;
                        }
                    } 
                }
                
                zombie.Die();
                Object.Destroy(zombie.gameObject);
            }
        }
    }
}
