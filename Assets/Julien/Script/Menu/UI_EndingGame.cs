using UnityEngine;
using UnityEngine.SceneManagement;

namespace Julien.Script.Menu
{
    public class UI_EndingGame : MonoBehaviour
    {
        public void ReturnMenu()
        {
            SceneManager.LoadScene("Julien/Scenes/MainMenu");
        }
    }
}
