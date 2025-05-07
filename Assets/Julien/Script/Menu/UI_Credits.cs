using UnityEngine;

namespace Julien.Script.Menu
{
    public class UI_Credits : MonoBehaviour
    {
        public GameObject CreditsMenu;
        public GameObject ReturnMenu;
        
        public void Retour()
        {
            CreditsMenu.SetActive(false);
            ReturnMenu.SetActive(true);
        }
    }
}
