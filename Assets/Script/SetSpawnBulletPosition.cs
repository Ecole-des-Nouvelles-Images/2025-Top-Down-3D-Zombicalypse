using System;
using UnityEngine;

namespace Script
{
    public class SetSpawnBulletPosition : MonoBehaviour
    {
        public void SetPosition(Vector3 newPosition)
        {
            gameObject.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
        }
    }
}
