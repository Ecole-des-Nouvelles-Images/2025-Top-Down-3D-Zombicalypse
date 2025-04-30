using UnityEngine;

namespace Script.Data.ZombieData
{
    [CreateAssetMenu(fileName = "ZombieData", menuName = "Scriptable Objects/ZombieData")]
    public class ZombieData : ScriptableObject
    {
        public int PirceZombie;
        
        public float MaxHealth;
        public float Damage;
        public float Speed;
        public float Acceleration;

        public float AttackRange;
        public float AttackSpeed;
    }
}
