using System;
using System.Collections;
using System.Collections.Generic;
using Julien.Script.Interface;
using NUnit.Framework;
using UnityEngine;

namespace Julien.Script
{
    public class HealingZone : MonoBehaviour
    {
        [SerializeField] private float _maxTime;
        [SerializeField] private float _timer;

        public float HealthValue;

        [SerializeField] private List<GameObject> _gameobject = new List<GameObject>();

        private void Start()
        {
            _timer = _maxTime;
            StartCoroutine("Destroy");
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                HealthTargets();
                _timer = _maxTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<ITakeHeal>() != null)
            {
                _gameobject.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<ITakeHeal>() != null)
            {
                _gameobject.Remove(other.gameObject);
            }
        }

        private void HealthTargets()
        {
            foreach (GameObject gameObject in _gameobject)
            {
                gameObject.GetComponent<ITakeHeal>().takeHeal(HealthValue);
            }
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(7);
            Destroy(gameObject);
        }
    }
}
