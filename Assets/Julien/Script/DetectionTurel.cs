using Julien.Script.TurelScripts;
using UnityEngine;

namespace Julien.Script
{
    public class DetectionTurel : MonoBehaviour
    {
        [SerializeField] private Turel _turel;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Zombie"))
            {
                Debug.Log("add zombie");
                _turel.Targets.Add(other.gameObject);
                _turel.Target = _turel.Targets[0];
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Zombie"))
            {
                _turel.Target = null;
                _turel.Targets.Remove(other.gameObject);
                if (_turel.Targets.Count > 0)
                {
                    _turel.Target = _turel.Targets[0];
                }
            }
        }
    }
}
