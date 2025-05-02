using System;
using System.Collections.Generic;
using Julien.Script.Struc;
using Julien.Script.TurelScripts;
using UnityEngine;

namespace Julien.Script
{
    public class HologramTurel : MonoBehaviour
    {
        public bool CanBeSetDoawn;
        public TurelWrap TurelWrap;
        
        
        [SerializeField] private List<Material> _materials;
        [SerializeField] private List<GameObject> _children;
        [SerializeField] private GameObject _AimCollider;

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                _children.Add(gameObject.transform.GetChild(i).gameObject);
                _AimCollider.transform.localScale = new Vector3(TurelWrap.Range, 1, TurelWrap.Distance);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log("Is Colliding" + " " + other.gameObject.name);
            CanBeSetDoawn = false;
            foreach (GameObject child in _children)
            {
                child.gameObject.GetComponent<MeshRenderer>().material = _materials[1];
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CanBeSetDoawn = true;
            foreach (GameObject child in _children)
            {
                child.gameObject.GetComponent<MeshRenderer>().material = _materials[0];
            }
        }
    }
}