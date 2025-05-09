using Script;
using UnityEngine;
using UnityEngine.UI;

namespace Julien.Script.HUD
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] private Generator _generator;

        [SerializeField] private Image _helthBarGenerator;
        private void Awake()
        {
            _generator = GameObject.FindWithTag("Generator").GetComponent<Generator>();
        }

        private void Start()
        {
            if (_generator != null)
            {
                _generator.OntakeDamage += SetHealthGenerator;
                Debug.Log(" ajouter l'evenet");
            }
        }

        private void OnDisable()
        {
            _generator.OntakeDamage -= SetHealthGenerator;
        }

        public void SetHealthGenerator(float currentHealth)
        {
            Debug.Log("l hud doit se mettre a jour");
            _helthBarGenerator.fillAmount = currentHealth / _generator.MaxHealth;
        }
    }
}
