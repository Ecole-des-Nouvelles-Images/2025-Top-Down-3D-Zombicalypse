using UnityEngine;
using UnityEngine.SceneManagement;

namespace Julien.Script.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject MainMenuPanel;
        
        public GameObject OptionsPanel;
        public GameObject CreditsPanel;
        
        public void Play()
        {
            Debug.Log("Play");
            SceneManager.LoadScene("Lobby");
        }
        public void OpenOptions()
        {
            Debug.Log("Options");
        }
        public void OpenCredit()
        {
            Debug.Log("Credit");
        }
        public void Quitter()
        {
            Debug.Log("Quitter");
            Application.Quit();
        }
    }
}
