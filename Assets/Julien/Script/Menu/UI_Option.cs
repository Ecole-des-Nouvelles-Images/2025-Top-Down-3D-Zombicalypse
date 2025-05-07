using UnityEngine;

namespace Julien.Script.Menu
{
    public class UI_Option : MonoBehaviour
    {
        public GameObject OptionPanel;

        public GameObject ReturnPanel;
        
        public void Return()
        {
            OptionPanel.SetActive(false);
            ReturnPanel.SetActive(true);
        }
    }
}
