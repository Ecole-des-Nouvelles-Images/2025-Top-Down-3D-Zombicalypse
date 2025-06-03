using UnityEngine;

namespace Julien.Script.Data.Player
{
    [CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
    public class PlayerData : ScriptableObject
    {
        public float health;
        public float Speed;
    }
}
