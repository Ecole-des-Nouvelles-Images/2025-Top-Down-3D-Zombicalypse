using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class TriggerAll : MonoBehaviour
    {
        public ZombieScript.Zombie Zombie;
        public List<GameObject> Objects;

        private void Start()
        {
            Zombie = gameObject.transform.parent.GetComponent<ZombieScript.Zombie>();
        }

        private void OnTriggerEnter(Collider Object)
        {
            Zombie.Object.Add(Object.gameObject);
        }

        private void OnTriggerExit(Collider Object)
        {
            Zombie.Object.Remove(Object.gameObject);
        }
    }
}
