using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Julien.Script.Menu
{
    public class UI_FirstSelected : MonoBehaviour
    {
        private GameObject _eventSystemGameObject;
        private EventSystem _eventSystem;

        [SerializeField] private GameObject Button;
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
