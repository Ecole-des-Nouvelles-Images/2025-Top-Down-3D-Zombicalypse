using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Julien.Script.Menu
{
    public class UIFirstSelected : MonoBehaviour
    {
        private GameObject _eventSystemGameObject;
        private EventSystem _eventSystem;

        [SerializeField] private GameObject Button;
        [SerializeField] private TMP_Text _endingText;
        private void Awake()
        {
            _eventSystemGameObject = GameObject.Find("EventSystem");
            _eventSystem = _eventSystemGameObject.GetComponent<EventSystem>();
        }
        private void OnEnable()
        {
            _eventSystem.SetSelectedGameObject(Button.gameObject);
        }

        private void Start()
        {
            _eventSystem.SetSelectedGameObject(Button.gameObject);
        }
    }
}
