using Julien.Script.Static;
using Script;
using UnityEngine;
using UnityEngine.UI;

namespace Julien.Script.HUD
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] private Image _helthBarGenerator;
        private void Start()
        {
            StaticAction.OntakedDamage += SetHealthGenerator;
            Debug.Log(" ajouter l'evenet");
        }

        private void OnDisable()
        {
            StaticAction.OntakedDamage -= SetHealthGenerator;
        }

        public void SetHealthGenerator(float currentHealth, float maxHealth)
        {
            Debug.Log("l hud doit se mettre a jour");
            _helthBarGenerator.fillAmount = currentHealth / maxHealth;
        }
    }
}
