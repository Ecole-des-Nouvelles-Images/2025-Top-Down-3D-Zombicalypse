using UnityEngine;

namespace Julien.Script.HUD
{
    public class LookCamera : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;

        private void Update()
        {
            if (_camera == null)
            {
                _camera = GameObject.FindWithTag("Camera");
            }
            Vector3 camForward = _camera.transform.forward;
            Vector3 camUp = _camera.transform.up;

            transform.LookAt(transform.position + camForward, camUp);
        }
    }
}
