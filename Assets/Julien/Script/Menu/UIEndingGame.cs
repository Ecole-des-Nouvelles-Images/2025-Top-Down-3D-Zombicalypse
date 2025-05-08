using UnityEngine;
using UnityEngine.SceneManagement;

namespace Julien.Script.Menu
{
    public class UIEndingGame : MonoBehaviour
    {
        public void ReturnMenu()
        {
            SceneManager.LoadScene("Julien/Scenes/MainMenu");
        }
    }
}
