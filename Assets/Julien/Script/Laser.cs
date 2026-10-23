using System;
using UnityEngine;

namespace Julien.Script
{
    public class Laser : MonoBehaviour
    {
        public LineRenderer LineRenderer;
        [SerializeField] private LayerMask _excludeLayer;

        private void Update()
        {
            SetLaser();
        }

        public void SetLaser()
        {
            LineRenderer.SetPosition(0, transform.position);

            float maxDistance = 100f;
            Vector3 endPosition = transform.position + transform.forward * maxDistance;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
            {
                endPosition = hit.point;

                if (hit.collider.isTrigger)
                {
                    RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, maxDistance);
                    System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

                    foreach (RaycastHit h in hits)
                    {
                        if (!h.collider.isTrigger)
                        {
                            endPosition = h.point;
                            break;
                        }
                    }
                }
            }

            LineRenderer.SetPosition(1, endPosition);
        }
    }
}
