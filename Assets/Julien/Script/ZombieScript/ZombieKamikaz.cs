using System.Collections;
using Julien.Script.Interface;
using Julien.Script.Static;
using Julien.Script.ZombieScript.States;
using Julien.Script.ZombieScript.States.Kamaikaz;
using UnityEngine;
namespace Julien.Script.ZombieScript
{
    public class ZombieKamikaz : Zombie
    {
        public bool AreExplosed;
        
        [SerializeField] private GameObject _firstParticle;
        [SerializeField] private GameObject _secondParticle;
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
            }
            
            switch (WantAttack)
            {
                case true when Dead:
                    CurrentState = new Dead();
                    break;
                case true when !CanAttack:
                    CurrentState = new GoAttackTarget();
                    break;
                case true when CanAttack:
                    CurrentState = new GoExplose();
                    if (!AreExplosed) StartCoroutine(DelayExplosion());
                    Debug.Log("Couroutine jouer");
                    AreExplosed = true;
                    break;
                case true when Explose:
                    break;
            }
            PlayScream();
            CurrentState.Execute(this);
        }

        public IEnumerator DelayExplosion()
        {
            Debug.Log("VA EXPLOSER");
            if (Dead)
            {
                Animator.SetBool("Die", true);
            }
            yield return new WaitForSeconds(TypeZombie.AttackSpeed);
            
            GameObject.FindWithTag("GameManager").GetComponent<RoundHundler>().ZombieToKillCount++;
            if (!Dead)
            {
                Instantiate(_firstParticle,transform.position,Quaternion.identity);
                Instantiate(_secondParticle,transform.position,Quaternion.identity);
                Destroy(gameObject);
                AreExplosed = true;
                Debug.Log("EXPLOSE PARTICLE");
            }
        }
        private void Start()
        {
            SetData();
            SetRoundBonusStat();
        }
    }
}
