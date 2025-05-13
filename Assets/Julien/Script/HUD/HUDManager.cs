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
            StaticAction.OntakedDamage += SetHealthGenerator;
            StaticAction.OnAddPoint += SetProgressBar;
            Debug.Log(" ajouter l'evenet");
        }

        private void OnDisable()
        {
            StaticAction.OntakedDamage -= SetHealthGenerator;
            StaticAction.OnAddPoint -= SetProgressBar;
        }

        public void SetHealthGenerator(float currentHealth, float maxHealth)
        {
            _helthBarGenerator.fillAmount = currentHealth / maxHealth;
            if (_helthBarGenerator.fillAmount <= 0)
            {
                Time.timeScale = 0;
                Debug.Log("Active Main menu button");
            }
        }

        public void SetProgressBar(float pointToadd)
        {
            GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gameManager.CurrentPoint += pointToadd;
            _progressBar.fillAmount = gameManager.CurrentPoint / gameManager.MaxProgressBar;
            
            
            
            if (_progressBar.fillAmount >= 1)
            {
                
            }
        }
        
        public void Pause()
        {
            PauseMenu.SetActive(true);
        }
    }
}
