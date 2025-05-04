using UnityEngine;
using UnityEngine.Serialization;

namespace Julien.Script.Data.Zombie
{
    [CreateAssetMenu(fileName = "ZombieData", menuName = "Scriptable Objects/ZombieData")]
    public class ZombieData : ScriptableObject
    {
        public int PriceZombie;
        
        public float MaxHealth;
        public float Damage;
        public float Speed;
        public float Acceleration;

        public float AttackRange;
        public float AttackSpeed;
    }
}
