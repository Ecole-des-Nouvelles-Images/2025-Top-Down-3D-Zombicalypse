using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Camera
{
    public class InfoForCamera : MonoBehaviour
    {
       [SerializeField] private CinemachineTargetGroup _cinemachineTargetGroup;
       [SerializeField] private List<GameObject> _targetList = new List<GameObject>();
       [SerializeField] private GameObject[] _targets;
       private void Awake()
       {
           _cinemachineTargetGroup = gameObject.GetComponent<CinemachineTargetGroup>();
       }

       private void Start()
       {
           StartCoroutine("Delay");
       }

       public IEnumerator Delay()
       {
           yield return new WaitForSeconds(3);
           SetTarget();
       }

       public void SetTarget()
       {
           _targets = GameObject.FindGameObjectsWithTag("Player");
       }
    }
}
