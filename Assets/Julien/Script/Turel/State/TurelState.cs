using UnityEngine;

namespace Script.Turel.State
{
    public abstract class TurelState : MonoBehaviour
    {
        public abstract void Execute(Turel turel);
    }
}
