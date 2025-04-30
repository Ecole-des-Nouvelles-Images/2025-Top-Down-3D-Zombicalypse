using UnityEngine;

namespace Script.Data.PlayerData
{
    [CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
    public class PlayerData : ScriptableObject
    {
        public float health;
        public float Speed;
    }
}
