using UnityEngine;
using UnityEngine.SceneManagement;

namespace Julien.Script.Menu
{
    public class UIMainMenu : MonoBehaviour
    {
        public GameObject MainMenuPanel;
        
        public GameObject OptionsPanel;
        public GameObject CreditsPanel;
        
        public void Play()
        {
            SceneManager.LoadScene("Lobby");
        }
        public void OpenOptions()
        {
            MainMenuPanel.SetActive(false);
            OptionsPanel.SetActive(true);
        }
        public void OpenCredit()
        {
            MainMenuPanel.SetActive(false);
            CreditsPanel.SetActive(true);
        }
        public void Quitter()
        {
            Debug.Log("Quitter");
            Application.Quit();
        }
    }
}
