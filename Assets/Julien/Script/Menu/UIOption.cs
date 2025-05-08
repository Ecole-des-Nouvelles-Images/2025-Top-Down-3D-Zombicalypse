using UnityEngine;

namespace Julien.Script.Menu
{
    public class UIOption : MonoBehaviour
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
