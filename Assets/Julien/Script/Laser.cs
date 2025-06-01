using System;
using UnityEngine;

namespace Julien.Script
{
    public class Laser : MonoBehaviour
    {
        public LineRenderer LineRenderer;

        private void Update()
        {
            SetLaser();
            // _lineRenderer.SetPosition(1, new Vector3(transform.localPosition.x  + 100, transform.localPosition.y, transform.localPosition.z));
        }

        public void SetLaser()
        {
            LineRenderer.SetPosition(0, transform.position);
            LineRenderer.SetPosition(1, new Vector3(transform.forward.x * 100, 1, transform.forward.z * 100));
        }
    }
}
