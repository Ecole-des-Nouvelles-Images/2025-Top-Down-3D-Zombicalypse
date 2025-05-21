using UnityEngine;

namespace Julien.Script.DebugScript
{
    public class TimeScale : MonoBehaviour
    {
        [Range(0.1f, 10)] public float timeScale;

        private void Update()
        {
            Time.timeScale = timeScale;
        }
    }
}
