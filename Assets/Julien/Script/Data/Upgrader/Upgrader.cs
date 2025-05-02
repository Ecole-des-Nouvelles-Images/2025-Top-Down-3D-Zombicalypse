using UnityEngine;
using UnityEngine.Serialization;

namespace Julien.Script.Data.Upgrader
{
    [CreateAssetMenu(fileName = "Upgrader", menuName = "Scriptable Objects/Upgrader")]
    public class Upgrader : ScriptableObject
    {
        public GameObject VisualGameObject;

        public float AddDamage;
        public float AddFireRate;
        public float AddMaxHealth;
        [FormerlySerializedAs("Range")] public float AddRange;
        [FormerlySerializedAs("Distance")] public float AddDistance;
    }
}
