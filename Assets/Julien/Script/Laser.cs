using System;
using UnityEngine;

namespace Julien.Script
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private void Update()
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.forward * 100);
            // _lineRenderer.SetPosition(1, new Vector3(transform.localPosition.x  + 100, transform.localPosition.y, transform.localPosition.z));
        }   
    }
}
