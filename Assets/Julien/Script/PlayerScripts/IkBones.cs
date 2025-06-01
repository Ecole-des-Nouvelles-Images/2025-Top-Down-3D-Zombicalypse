using System;
using UnityEngine;

namespace Julien.Script.PlayerScripts
{
    public class IkBones : MonoBehaviour
    {
        [SerializeField] private HandingObject _handingObject;
        
        [SerializeField] private GameObject _rightHandGismo;
        [SerializeField] private GameObject _leftHandGismo;

        [SerializeField] private Transform _rightHandPlacement;
        [SerializeField] private Transform _leftHandPlacement;

        private void Start()
        {
            MoveHands();
            Debug.Log("Start");
        }

        private void Update()
        {
            ChangeHandsPlacement();
            MoveHands();
        }

        [ContextMenu("PlaceHands")]
        public void MoveHands()
        {
            _rightHandGismo.transform.position = _rightHandPlacement.position;
            _rightHandGismo.transform.rotation = _rightHandPlacement.rotation;
            
            _leftHandGismo.transform.position = _leftHandPlacement.position;
            _leftHandGismo.transform.rotation = _leftHandPlacement.rotation;
        }

        public void ChangeHandsPlacement()
        {
            Transform rightHandPlacement = _handingObject.WeaponMesh.transform.Find("RightPlacement");
            Transform leftHandPlacement = _handingObject.WeaponMesh.transform.Find("LeftPlacement");

            _rightHandPlacement = rightHandPlacement;
            _leftHandPlacement = leftHandPlacement;
            
        }
    }
}
