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

        private void Update()
        {
            PlaceHands();
        }

        [ContextMenu("PlaceHands")]
        public void PlaceHands()
        {
            Debug.Log("PlaceHands");
            Transform rightHandPlacement = _handingObject.WeaponMesh.transform.Find("RightPlacement");
            Transform leftHandPlacement = _handingObject.WeaponMesh.transform.Find("LeftPlacement");

            _rightHandPlacement = rightHandPlacement;
            _leftHandPlacement = leftHandPlacement;

            _rightHandGismo.transform.position = rightHandPlacement.position;
            _rightHandGismo.transform.rotation = rightHandPlacement.rotation;
            
            _leftHandGismo.transform.position = leftHandPlacement.position;
            _leftHandGismo.transform.rotation = leftHandPlacement.rotation;
        }
    }
}
