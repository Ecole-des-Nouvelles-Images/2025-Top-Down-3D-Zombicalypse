using UnityEngine;
using UnityEngine.Serialization;

namespace Julien.Script.PlayerScripts
{
    public class PlayerScore : MonoBehaviour
    {
        public float Score;
        public int ZombieKilled;
        public float DamageCount;
        [FormerlySerializedAs("KoCount")] [FormerlySerializedAs("DeadCount")] public int DieCount;
        
        public void CalculScore()
        {
            Score += ZombieKilled * 5;
            Score += DamageCount;
            Score -= DieCount * 200;
        }
    }
}
