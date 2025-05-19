using UnityEngine;
using UnityEngine.UI;

namespace Julien.Script.HUD.OnTopPlayer
{
    public class ReloadingBar : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private bool _play;
        private float _maxReloadTime;
        private float _reloadTime;
        
        public void SetReloardBoar(float maxReloadTime)
        {
            gameObject.SetActive(true);
            _image.fillAmount = 0;
            _maxReloadTime = maxReloadTime;
            _reloadTime = _maxReloadTime;
            _play = true;
        }

        private void Update()
        {
            if (_play)
            {
                _reloadTime -= Time.deltaTime;
                _image.fillAmount = _reloadTime / _maxReloadTime;
                if (_reloadTime <= 0)
                {
                    _play = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
