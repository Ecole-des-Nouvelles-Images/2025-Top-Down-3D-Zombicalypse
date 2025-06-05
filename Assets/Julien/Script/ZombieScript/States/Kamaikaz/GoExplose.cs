using Julien.Script.Interface;
using UnityEngine;

namespace Julien.Script.ZombieScript.States.Kamaikaz
{
    public class GoExplose : ZombieState
    {
        public override void Execute(Zombie zombie)
        {
            zombie.NavMeshAgent.speed = 0f;
            if (zombie.Dead == false)
            {
                zombie.AttackSpeed -= Time.deltaTime;
            }
            else
            {
                zombie.CurrentState = new Dead();
            }
            zombie.Animator.SetBool("Run", false);
            zombie.Animator.SetTrigger("GoExplose");
            
            if (zombie.AttackSpeed <= 0)
            {
                zombie.Explose = true;
                if (zombie.Object != null)
                {
                    for (int i = zombie.Object.Count - 1; i >= 0; i--)
                    {
                        GameObject obj = zombie.Object[i];
                        if (obj.GetComponent<ITakeDamage>() != null)
                        {
                            obj.GetComponent<ITakeDamage>().takeDamage(zombie.Damage);
                            zombie.Object.RemoveAt(i);
                            Debug.Log("ITake damage");
                        }
                    }
                }
                SoundManager.Instance.PlaySound(SoundManager.Instance.gameObject, SoundManager.Instance.Explosion, 0.4f);
                zombie.Die(false);
            }
        }
    }
}
