using UnityEngine;

namespace Julien.Script.PlayerScripts
{
    public class PlayerScore : MonoBehaviour
    {
        public float Score;
        public int ZombieKilled;
        public float DamageCount;
        public int DeadCount;
        
        public void CalculScore()
        {
            Score += ZombieKilled * 5;
            Score += DamageCount;
            Score -= DeadCount * 200;
        }
    }
}
