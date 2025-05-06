using UnityEngine;

namespace Julien.Script.DebugScript
{
    public class TimeScale : MonoBehaviour
    {
        [Range(1, 10)] public float timeScale;

        private void Update()
        {
            Time.timeScale = timeScale;
        }
    }
}
