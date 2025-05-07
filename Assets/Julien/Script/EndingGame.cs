using UnityEngine;

namespace Julien.Script
{
    public class EndingGame : MonoBehaviour
    {
        [SerializeField] private GameObject _EndingPanel;
        
        public void EndGame()
        {
            _EndingPanel.SetActive(true);
        }
    }
}
