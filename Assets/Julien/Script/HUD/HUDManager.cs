using System;
using Julien.Script.Static;
using UnityEngine;
using UnityEngine.UI;

namespace Julien.Script.HUD
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] private Image _helthBarGenerator;
        [SerializeField] private GameObject _returnMenuButton;
        [SerializeField] private Image _progressBar;
        public GameObject PauseMenu;
        
        private void Start()
        {
            EventBus.OntakedDamage += SetHealthGenerator;
            EventBus.OnAddPoint += SetProgressBar;
        }

        private void OnDisable()
        {
            EventBus.OntakedDamage -= SetHealthGenerator;
            EventBus.OnAddPoint -= SetProgressBar;
        }

        public void SetHealthGenerator(float currentHealth, float maxHealth)
        {
            if (!_helthBarGenerator) return;
            _helthBarGenerator.fillAmount = currentHealth / maxHealth;
            if (_helthBarGenerator.fillAmount <= 0)
            {
                Time.timeScale = 0;
                Debug.Log("Active Main menu button");
            }
        }

        public void SetProgressBar(float pointToadd)
        {
            if (!_progressBar)
            {
                return;
            }
            GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gameManager.CurrentPoint += pointToadd;
             _progressBar.fillAmount = gameManager.CurrentPoint / gameManager.MaxProgressBar;
            
            if (_progressBar.fillAmount >= 1)
            {
                gameManager.CurrentPoint = 0;
                gameManager.MaxProgressBar += 10;
                gameManager.SpawnWeapon();
                SetProgressBar(0);
            }
        }
        
        public void Pause()
        {
            PauseMenu.SetActive(true);
        }
    }
}
