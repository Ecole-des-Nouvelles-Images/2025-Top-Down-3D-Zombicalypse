using System;
using UnityEngine;
using UnityEngine.UI;

namespace Julien.Script.HUD
{
    public class HUDPlayerHealth : MonoBehaviour
    {
        [SerializeField] public Image _healthImage;

        private void Awake()
        {
            _healthImage = GetComponent<Image>();
        }

        public void SetHealthBarHUD(float currentHealth, float maxHealth)
        {
            _healthImage.fillAmount = currentHealth / maxHealth;
        }
    }
}
